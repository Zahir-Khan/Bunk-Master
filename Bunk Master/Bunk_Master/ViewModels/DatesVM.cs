using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bunk_Master
{
    public class DatesVM : BaseVM 
    {
        private Database db_dates { get; set; }
        readonly Database db_overdate;
        public DateTime date { get; set; } = DateTime.Today;
        public string dt_ab { get; set; } = "0";
        public string dt_tot { get; set; } = "0";

        double tot_ab { get; set; } = 0;
        double tot { get; set; } = 0;
        double pc { get; set; } = 0;


        public ICommand AddCommand { get; set; }



        public ObservableCollection<DatesModel> Dates { get; set; }
        public ObservableCollection<DatesModel> OverDates { get; set; }
        public ObservableCollection<PercentModel> Percent { get; set; }
        public ObservableCollection<PercentModel> Predict { get; set; }
        public ObservableCollection<PercentModel> MinInstance { get; set; }
        public ObservableCollection<TelemetModel> TelemetInstance { get; set; }
        //Overall Collections
        public ObservableCollection<PercentModel> Opredict { get; set; }
        public ObservableCollection<PercentModel> OminInstance { get; set; }
        public ObservableCollection<PercentModel> Opercent { get; set; }
        public ObservableCollection<TelemetModel> OtelemetInstance { get; set; }

        int int_id;
        DateTime int_date;

        public DatesVM()
        {
            AddCommand = new Command<bool>(Add);
            Percent = new ObservableCollection<PercentModel>();
            Dates = new ObservableCollection<DatesModel>();
            OverDates = new ObservableCollection<DatesModel>();
            Predict = new ObservableCollection<PercentModel>();
            MinInstance = new ObservableCollection<PercentModel>();
            TelemetInstance = new ObservableCollection<TelemetModel>();
            Opredict = new ObservableCollection<PercentModel>();
            OminInstance = new ObservableCollection<PercentModel>();
            Opercent = new ObservableCollection<PercentModel>();
            OtelemetInstance = new ObservableCollection<TelemetModel>();
            db_overdate = new Database("overdatesdb"); // Overdate database
            db_overdate.CreateTable<DatesModel>();

        }

        

        

        public void SelDB(int id, bool calc)
        {
            int_id = id;
            db_dates = new Database("datesdb" + id);
            db_dates.CreateTable<DatesModel>();

            Calculations(calc);
        }

        public void NewDB(int id)
        {
            int_id = id;
            db_dates = new Database("datesdb" + id);
            db_dates.CreateTable<DatesModel>();
            dt_tot = ViewModel.ClassVMInstance.totnos;
            dt_ab = (int.Parse(dt_tot) - int.Parse(ViewModel.ClassVMInstance.presentnos)).ToString();
            //Add(false);//Adding direct code so that add is used only for updating dates
            var dtab = int.Parse(dt_ab);
            var dttot = int.Parse(dt_tot);
            AddOverDates(date, dtab, dttot);
            var dates = new DatesModel
            {
                date = date,
                dt_ab = dtab,
                dt_tot = dttot
            };
            db_dates.SaveItem(dates);
            Dates.Add(dates);
            //ShowAllRecords();

        }



        void AddOverDates(DateTime date, int dtab, int dttot)
        {
            var defaultOverDates = new DatesModel { };
            var check = db_overdate.Query<DatesModel>("SELECT * FROM DatesModel WHERE date = ? ", new object[] { date });
            if (check.Count() == 0) //Just Save a row with default values if it doesnt exist
            {
                defaultOverDates = new DatesModel
                {
                    date = date,
                    dt_ab = 0,
                    dt_tot = 0
                };
                db_overdate.ExecuteQuery("INSERT INTO DatesModel ( dt_ab , dt_tot , date ) VALUES ( ? , ? , ? )", new object[] { 0, 0, date });
            }

            defaultOverDates = new DatesModel
            {
                date = date,
                dt_ab = dtab,
                dt_tot = dttot
            };
            db_overdate.ExecuteQuery("UPDATE DatesModel SET dt_ab = dt_ab + ? , dt_tot = dt_tot + ? WHERE date = ?", new object[] { dtab, dttot, date });
            RaisePropertyChanged(nameof(db_overdate));
            ShowAllOverDates();

        }

        public void Sched(int id, string tot) //daily scheduler backend db updates called once per id - Just adds a new entry every new day
        {
            SelDB(id, true);

            var res = db_dates.Query<DatesModel>("SELECT * FROM DatesModel WHERE date = ?", new object[] { DateTime.Today });
            if (res.Count == 0)
            {
                var dttot = int.Parse(tot);
                var dates = new DatesModel
                {
                    date = DateTime.Today,
                    dt_ab = 0,
                    dt_tot = dttot
                };

                AddOverDates(DateTime.Today, 0, dttot);

                ViewModel.ClassVMInstance.UpdateNos(id, dttot, dttot);
                db_dates.SaveItem(dates);
                Dates.Add(dates);
            }

            else
            {
                //do nothing , if res.count > 1 then its a problem of multiple dates entry

            }

            RaisePropertyChanged(nameof(Dates));
            Calculations(false);
        }

        void Add(bool calc) //Handle Failed If blocks
        {
            int dtab_t = 0;
            int dttot_t = 0;
            int pres_diff;
            int tot_diff;

            var res = db_dates.Query<DatesModel>("SELECT * FROM DatesModel WHERE date = ?", new object[] { date });

            if (res.Count == 1) //dt-info gets updated
            {
                var itemString = res[0].ToString();
                var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
                int id_strd = int.Parse(columns[0]);
                DateTime dt_strd = DateTime.Parse(columns[1]);
                int ab_strd = int.Parse(columns[2]);
                int tot_strd = int.Parse(columns[3]);
                dtab_t = int.Parse(dt_ab);
                dttot_t = int.Parse(dt_tot);
                tot_diff = dttot_t - tot_strd; // new - old +ve if increased
                pres_diff = (dttot_t - tot_strd) - (dtab_t - ab_strd); //new - old (P=A-T) +ve if increased

                AddOverDates(dt_strd, dtab_t, dttot_t);

                var dates = new DatesModel
                {
                    ID = id_strd,
                    date = date,
                    dt_ab = dtab_t,
                    dt_tot = dttot_t
                };
                ViewModel.ClassVMInstance.UpdateNos(int_id, pres_diff, tot_diff);
                db_dates.SaveItem(dates);
                Dates.Add(dates);


            }
            else if (res.Count == 0) //new dt-info is added
            {
                var dtab = int.Parse(dt_ab);
                var dttot = int.Parse(dt_tot);
                var dates = new DatesModel
                {
                    date = date,
                    dt_ab = dtab,
                    dt_tot = dttot
                };

                AddOverDates(date, dtab, dttot);

                ViewModel.ClassVMInstance.UpdateNos(int_id, dttot - dtab, dttot);
                db_dates.SaveItem(dates);
                Dates.Add(dates);
            }

            else
            {
                throw new Exception("DATABASE CORRUPTION - Multiple Date Entry"); //Need to make a new class to display errors/messages
            }

            RaisePropertyChanged(nameof(Dates));
            ClearForm();
            Calculations(calc);


        }

        //Increases only absent
        public void Missed(DateTime seldt) //Need to impliment long press extra features
        {
            int id;
            int dtab_t;
            int dttot_t;
            List<DatesModel> res = new List<DatesModel>();
            res = db_dates.Query<DatesModel>("SELECT * FROM DatesModel WHERE date = ?", new object[] { seldt });
            if (res.Count > 0) //handle fail condition
            {
                var itemString = res[0].ToString();
                var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
                id = int.Parse(columns[0]);
                dtab_t = int.Parse(columns[2]);
                dttot_t = int.Parse(columns[3]);
                if (dtab_t < dttot_t)
                {
                    dtab_t += 1;
                    var record = new DatesModel
                    {
                        ID = id,
                        date = seldt,
                        dt_ab = dtab_t,
                        dt_tot = dttot_t
                    };

                    db_dates.SaveItem(record);
                    ViewModel.ClassVMInstance.UpdateNos(int_id, -1, 0);
                    AddOverDates(date, -1, 0);//match the data
                    ClearForm();
                    ShowAllRecords();
                }
            }
            else
            {
                var dtab = int.Parse(dt_ab);
                var dttot = int.Parse(dt_tot);
                var dates = new DatesModel
                {
                    date = date,
                    dt_ab = dtab,
                    dt_tot = dttot
                };

                db_dates.SaveItem(dates);
                Dates.Add(dates);
                ViewModel.ClassVMInstance.UpdateNos(int_id, dttot - dtab, dttot);
                AddOverDates(date, dttot - dtab, dttot);//match the data
                RaisePropertyChanged(nameof(Dates));
                ClearForm();
                ShowAllRecords();
            }
        }

        //tot get dec (in some cond ab get dec)
        public void Cancelled(DateTime seldt) //Need to impliment long press extra features
        {
            int id;
            int dtab_t;
            int dttot_t;
            List<DatesModel> res = new List<DatesModel>();
            res = db_dates.Query<DatesModel>("SELECT * FROM DatesModel WHERE date = ?", new object[] { seldt });
            if (res.Count > 0)
            {
                var itemString = res[0].ToString();
                var columns = itemString.Split(',').Select(i => i.Trim()).ToList();

                if ((int.TryParse(columns[0], out id)) && (int.TryParse(columns[2], out dtab_t)) &&
                    (int.TryParse(columns[3], out dttot_t)))    //Fail condition not handled, make more concise
                {
                    if (dtab_t < dttot_t)
                    {
                        if (dttot_t > 0)
                        {
                            dttot_t--;
                            ViewModel.ClassVMInstance.UpdateNos(int_id, 0, -1);
                            AddOverDates(date, 0, -1);//match the data
                        }
                    }
                    else if (dttot_t > 0)
                    {
                        dttot_t--;
                        dtab_t--;
                        ViewModel.ClassVMInstance.UpdateNos(int_id, -1, -1);
                        AddOverDates(date, -1, -1);//match the data
                    }
                    var record = new DatesModel
                    {
                        ID = id,
                        date = seldt,
                        dt_ab = dtab_t,
                        dt_tot = dttot_t
                    };

                    db_dates.SaveItem(record);
                }
                ClearForm();

            }
            else
            {
                var dtab = int.Parse(dt_ab);
                var dttot = int.Parse(dt_tot);
                var dates = new DatesModel
                {
                    date = date,
                    dt_ab = dtab,
                    dt_tot = dttot
                };
                ViewModel.ClassVMInstance.UpdateNos(int_id, dttot - dtab, dttot);
                AddOverDates(date, 0, -1);//match the data
                db_dates.SaveItem(dates);
                Dates.Add(dates);
                RaisePropertyChanged(nameof(Dates));
                ClearForm();
                ShowAllRecords();
            }

            ShowAllRecords();
        }

        //tot get inc
        public void Extra(DateTime seldt)//Need to impliment long press extra features
        {
            int id;
            int dtab_t;
            int dttot_t;
            List<DatesModel> res = new List<DatesModel>();
            res = db_dates.Query<DatesModel>("SELECT * FROM DatesModel WHERE date = ?", new object[] { seldt });
            if (res.Count > 0)
            {
                var itemString = res[0].ToString();
                var columns = itemString.Split(',').Select(i => i.Trim()).ToList();

                if ((int.TryParse(columns[0], out id)) && (int.TryParse(columns[2], out dtab_t)) &&
                    (int.TryParse(columns[3], out dttot_t)))        //Fail condi not handled
                {
                    dttot_t += 1;
                    var record = new DatesModel
                    {
                        ID = id,
                        date = seldt,
                        dt_ab = dtab_t,
                        dt_tot = dttot_t
                    };
                    db_dates.SaveItem(record);
                    ViewModel.ClassVMInstance.UpdateNos(int_id, 1, 1);//asuming extra class was attended
                }
                ClearForm();


            }
            else
            {
                var dtab = int.Parse(dt_ab);
                var dttot = int.Parse(dt_tot);
                var dates = new DatesModel
                {
                    date = date,
                    dt_ab = dtab,
                    dt_tot = dttot
                };

                db_dates.SaveItem(dates);
                Dates.Add(dates);
                ViewModel.ClassVMInstance.UpdateNos(int_id, 1, 1);

                RaisePropertyChanged(nameof(Dates));
                ClearForm();
                ShowAllRecords();
            }
            AddOverDates(date, 1, 1); //call function only once regardless of outcome
            ShowAllRecords();
        }

        public void DayAbsent()
        {
            var items = ViewModel.ClassVMInstance.GetItemIds();
            foreach (var item in items)
            {
                SelDB(item.Id, false);
                while (item.Rdata > 0)
                {
                    Missed(DateTime.Today);
                    item.Rdata--;
                }

            }
        }

        void Delete(object obj) //NOT INTEGRATED WITH OVERMODEL, NEVER USED
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            int id;
            int ab_strd = int.Parse(columns[2]);
            int tot_strd = int.Parse(columns[3]);

            if (int.TryParse(columns[0], out id))
            {
                db_dates.DeleteItem<DatesModel>(id);
                ViewModel.ClassVMInstance.UpdateNos(id, -(tot_strd - ab_strd), -tot_strd);//NOT SURE if it works
                Calculations(true);
            }
        }

        public void DeleteDtRange(DateTime frmdt, DateTime todt, int id)
        {
            SelDB(id, true);
            db_dates.Query<DatesModel>("DELETE FROM DatesModel WHERE date BETWEEN ? AND ?", new object[] { frmdt, todt });
            var sumlist = db_dates.Query<SumModel>("SELECT TOTAL(dt_ab) as \"Sum_ab\" ,TOTAL(dt_tot) as \"Sum_tot\" FROM DatesModel WHERE date BETWEEN ? AND ?", new object[] { frmdt, todt });
            int sumtot = 0;
            int sumpres = 0;
            if (sumlist.Count == 1)
            {
                sumtot = sumlist[0].Sum_tot;
                sumpres = sumtot - sumlist[0].Sum_ab;
            }
            var datesdb = db_dates.GetItems<DatesModel>();
            foreach (var datesmodel in datesdb)
            {
                if (datesmodel.date <= todt && datesmodel.date >= frmdt)
                {
                    AddOverDates(datesmodel.date, -datesmodel.dt_ab, -datesmodel.dt_tot);
                }
            }
            ViewModel.ClassVMInstance.UpdateNos(id, -sumpres, -sumtot);

            Calculations(true);

        }



        //MUST LINK CLASSVM AND DATESVM
        //public void DeleteAll()
        // {
        //     db_dates.DeleteAll<DatesModel>();
        //     Dates.Clear();
        // }

        public void DeleteDB(int id)
        {
            SelDB(id, false);
            var datesdb = db_dates.GetItems<DatesModel>();
            foreach (var datesmodel in datesdb)
            {
                AddOverDates(datesmodel.date, -datesmodel.dt_ab, -datesmodel.dt_tot);
            }
            db_dates.DeleteDatabase();
            //ShowAllRecords();
        }

        IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }


        public void PercentCalc() //I think i can make this better
        {
            Percent.Clear();
            tot = 0;
            tot_ab = 0;
            pc = 0;
            int i = 0;
            var sortedcoll = Dates.OrderBy(x => x.date).ToList();
            var count = sortedcoll.Count;
            var strdt = DateTime.Parse(ViewModel.ClassVMInstance.GetRow(int_id)[6]);
            Percent.Add(new PercentModel() { gpercent = 0.00, gdate = strdt.AddDays(-1) });
            foreach (DateTime day in EachDay(strdt, DateTime.Today))
            {
                if (i >= count)
                {
                    i = count - 1;
                }

                if (day == sortedcoll[i].date)
                {
                    tot_ab += (double)sortedcoll[i].dt_ab;
                    tot += (double)sortedcoll[i].dt_tot;
                    pc = (1 - (tot_ab / tot)) * 100.00;
                    Percent.Add(new PercentModel()
                    {
                        gpercent = Math.Round(pc, 2),
                        gdate = day,
                    });
                    i++;
                }
                else
                {

                    Percent.Add(new PercentModel()
                    {
                        gpercent = Math.Round(pc, 2),
                        gdate = day,
                    });
                }

            }
        }

        void Prediction()
        {

            var itot_ab = tot_ab;
            var itot = tot;
            var min = double.Parse(ViewModel.ClassVMInstance.GetRow(int_id)[4]);
            var cut = ViewModel.ClassVMInstance.GetRow(int_id)[5].Split('.').Select(i => int.Parse(i.Trim())).ToList();
            double ipc = pc;
            int_date = DateTime.Today.AddDays(1);

            Predict.Clear();
            if (ipc > min && ipc != double.NaN)
            {
                Predict.Add(new PercentModel()
                {
                    gdate = DateTime.Today,
                    gpercent = ipc
                });
                for (int i = 0; i < 31 && ipc > min; i++)
                {
                    if (cut[((int)int_date.DayOfWeek + 6) % 7] > 0)
                    {
                        var itemp = cut[((int)int_date.DayOfWeek + 6) % 7];
                        itot_ab += itemp;
                        itot += itemp;
                        ipc = (1 - (itot_ab / itot)) * 100;
                        Predict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }
                    else
                    {
                        Predict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }

                    int_date = int_date.AddDays(1);
                }
            }

            else if (ipc < min && ipc != double.NaN)
            {
                Predict.Add(new PercentModel()
                {
                    gdate = DateTime.Today,
                    gpercent = ipc
                });
                for (int i = 0; i < 31 && ipc < min; i++)
                {
                    if (cut[((int)int_date.DayOfWeek + 6) % 7] > 0)
                    {
                        var itemp = cut[((int)int_date.DayOfWeek + 6) % 7];
                        itot += itemp;
                        ipc = (1 - (itot_ab / itot)) * 100;
                        Predict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }
                    else
                    {
                        Predict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }

                    int_date = int_date.AddDays(1);
                }
            }
            else { int_date = int_date.AddDays(6); }
        }

        public void MinLine()
        {
            MinInstance.Clear();
            var mincut = ViewModel.ClassVMInstance.GetRow(int_id)[4];
            var strdt = DateTime.Parse(ViewModel.ClassVMInstance.GetRow(int_id)[6]);
            MinInstance.Add(new PercentModel() { gpercent = int.Parse(mincut), gdate = strdt.AddDays(-1) });
            MinInstance.Add(new PercentModel() { gpercent = int.Parse(mincut), gdate = int_date });
        }

        public void Telemetry()
        {
            int classcount = 0; //class count of future line
            //var rdata = ViewModel.ClassVMInstance.Records.ToList()[int_id-1].repeat_data.Split('.').Select(i => double.Parse(i.Trim())).ToList(); //repeat data Mo-Su(0-6)
            var rdata = ViewModel.ClassVMInstance.GetRow(int_id)[5].Split('.').Select(i => double.Parse(i.Trim())).ToList();//repeat data Mo-Su(0-6)
            double tdydate = ((double)Predict.First().gdate.DayOfWeek + 6) % 7;//todays date 
            double ystprcnt = Percent[Percent.Count - 2].gpercent; //yesterdays percent
            double tdyrdata = rdata[(int)((tdydate) % 7)];//todays rdata
            double tomrdata = rdata[((int)(tdydate + 1) % 7)];//tomo rdata
            double tdyprcnt = Percent[Percent.Count - 1].gpercent;//todays percent
            double tdytot = double.Parse(ViewModel.ClassVMInstance.GetRow(int_id)[2]);//todays total (total nos)
            double tdypres = double.Parse(ViewModel.ClassVMInstance.GetRow(int_id)[1]); ;//todays total pres nos
            double tomprcnt = ((tdypres + tomrdata) / (tdytot + tomrdata)) * 100;//tomo percent
            for (int i = 1; i < Predict.Count; i++)
            {
                int temp = (int)tdydate + 1;
                classcount += (int)rdata[((temp) % 7)];
                temp++;
            }
            classcount -= 1;
            if (Predict.First().gpercent > double.Parse(ViewModel.ClassVMInstance.GetRow(int_id)[4])) //negative if more than min line
            {
                classcount *= -1;
            }

            double tdyatnd = (((tdypres + tdyrdata) / (tdytot + tdyrdata)) * 100) - ystprcnt; //todays percent if attended
            double tomatnd = tomprcnt - tdyprcnt; //tomo percent if attended

            double tdybnk = (((tdypres - tdyrdata) / (tdytot - tdyrdata)) * 100) - ystprcnt;//todys % if bunked (*NOT CORRECT* )
            double tombnk = ((tdypres / (tdytot + tomrdata)) * 100) - tdyprcnt;//tomo % if bunked(*NOT CORRECT* didnt take into account that todays attendance is added later in the day)

            var telemet = new TelemetModel
            {
                Daycount = classcount,
                Tdyattnd = Math.Round(tdyatnd, 2),
                Tdybnk = Math.Round(tdybnk, 2),
                Tomattnd = Math.Round(tomatnd, 2),
                Tombnk = Math.Round(tombnk, 2)
            };
            TelemetInstance.Clear();
            TelemetInstance.Add(telemet);
        }

        public void Omethod()
        {
            ShowAllOverDates();
            //MINLINE
            OminInstance.Clear();
            var mincut = ViewModel.ClassVMInstance.Overmincut;
            var datelist = ViewModel.ClassVMInstance.DatesVMInstance.db_overdate.Query<DatesModel>("SELECT date FROM DatesModel ORDER BY date ASC", new object[] { });
            var strdt = datelist.FirstOrDefault().date;
            var enddt = datelist.LastOrDefault().date;
            OminInstance.Add(new PercentModel() { gpercent = int.Parse(mincut), gdate = strdt.AddDays(-1) });
            OminInstance.Add(new PercentModel() { gpercent = int.Parse(mincut), gdate = DateTime.Today.AddDays(1) });

            //PERCENT
            Opercent.Clear();
            tot = 0;
            tot_ab = 0;
            pc = 0;
            int nodeCount= 0;
            var sortedcoll = OverDates.OrderBy(x => x.date).ToList();
            var count = sortedcoll.Count;
            Opercent.Add(new PercentModel() { gpercent = 0.00, gdate = strdt.AddDays(-1) });
            foreach (DateTime day in EachDay(strdt, DateTime.Today))
            {
                if (nodeCount >= count)
                {
                    nodeCount = count - 1;
                }

                if (day == sortedcoll[nodeCount].date)
                {
                    tot_ab += (double)sortedcoll[nodeCount].dt_ab;
                    tot += (double)sortedcoll[nodeCount].dt_tot;
                    pc = (1 - (tot_ab / tot)) * 100.00;
                    Opercent.Add(new PercentModel()
                    {
                        gpercent = Math.Round(pc, 2),
                        gdate = day,
                    });
                    nodeCount++;
                }
                else
                {

                    Opercent.Add(new PercentModel()
                    {
                        gpercent = Math.Round(pc, 2),
                        gdate = day,
                    });
                }

            }

            //PREDICT

            var itot_ab = tot_ab;
            var itot = tot;
            var min = double.Parse(mincut);
            var cut = ViewModel.ClassVMInstance.GetOverdb().repeat_data.Split('.').Select(k => int.Parse(k.Trim())).ToList();
            double ipc = pc;
            int_date = DateTime.Today.AddDays(1);

            Opredict.Clear();
            if (ipc > min && ipc != double.NaN)
            {
                Opredict.Add(new PercentModel()
                {
                    gdate = DateTime.Today,
                    gpercent = ipc
                });
                for (int j = 0; j < 31 && ipc > min; j++)
                {
                    if (cut[((int)int_date.DayOfWeek + 6) % 7] > 0)
                    {
                        var itemp = cut[((int)int_date.DayOfWeek + 6) % 7];
                        itot_ab += itemp;
                        itot += itemp;
                        ipc = (1 - (itot_ab / itot)) * 100;
                        Opredict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }
                    else
                    {
                        Opredict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }

                    int_date = int_date.AddDays(1);
                }
            }

            else if (ipc < min && ipc != double.NaN)
            {
                Opredict.Add(new PercentModel()
                {
                    gdate = DateTime.Today,
                    gpercent = ipc
                });
                for (int j = 0; j < 31 && ipc < min; j++)
                {
                    if (cut[((int)int_date.DayOfWeek + 6) % 7] > 0)
                    {
                        var itemp = cut[((int)int_date.DayOfWeek + 6) % 7];
                        itot += itemp;
                        ipc = (1 - (itot_ab / itot)) * 100;
                        Opredict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }
                    else
                    {
                        Opredict.Add(new PercentModel()
                        {
                            gdate = int_date,
                            gpercent = ipc
                        });
                    }

                    int_date = int_date.AddDays(1);
                }
            }
            else { int_date = int_date.AddDays(6); }

            //TELEMETRY

            int classcount = 0; //class count of future line
            var rdata = ViewModel.ClassVMInstance.GetOverdb().repeat_data.Split('.').Select(i => double.Parse(i.Trim())).ToList();//repeat data Mo-Su(0-6)
            double tdydate = ((double)Opredict.First().gdate.DayOfWeek + 6) % 7;//todays date 
            double ystprcnt = Opercent[Opercent.Count - 2].gpercent; //yesterdays percent
            double tdyrdata = rdata[(int)((tdydate) % 7)];//todays rdata
            double tomrdata = rdata[((int)(tdydate + 1) % 7)];//tomo rdata
            double tdyprcnt = Opercent[Opercent.Count - 1].gpercent;//todays percent
            double tdytot = (ViewModel.ClassVMInstance.GetOverdb().totnos);//todays total (total nos)
            double tdypres = (ViewModel.ClassVMInstance.GetOverdb().presentnos); ;//todays total pres nos
            double tomprcnt = ((tdypres + tomrdata) / (tdytot + tomrdata)) * 100;//tomo percent
            for (int i = 1; i < Opredict.Count; i++)
            {
                int temp = (int)tdydate + 1;
                classcount += (int)rdata[((temp) % 7)];
                temp++;
            }
            classcount -= 1;
            if (Opredict.First().gpercent > (ViewModel.ClassVMInstance.GetOverdb().mincutoff)) //negative if more than min line
            {
                classcount *= -1;
            }

            double tdyatnd = (((tdypres + tdyrdata) / (tdytot + tdyrdata)) * 100) - ystprcnt; //todays percent if attended
            double tomatnd = tomprcnt - tdyprcnt; //tomo percent if attended

            double tdybnk = (((tdypres - tdyrdata) / (tdytot - tdyrdata)) * 100) - ystprcnt;//todys % if bunked (*NOT CORRECT* )
            double tombnk = ((tdypres / (tdytot + tomrdata)) * 100) - tdyprcnt;//tomo % if bunked(*NOT CORRECT* didnt take into account that todays attendance is added later in the day)

            var otelemet = new TelemetModel
            {
                Daycount = classcount,
                Tdyattnd = Math.Round(tdyatnd, 2),
                Tdybnk = Math.Round(tdybnk, 2),
                Tomattnd = Math.Round(tomatnd, 2),
                Tombnk = Math.Round(tombnk, 2)
            };
            OtelemetInstance.Clear();
            
            OtelemetInstance.Add(otelemet);
            
        }

        void ShowAllRecords()
        {
            Dates.Clear();
            var datesdb = db_dates.GetItems<DatesModel>();
            foreach (var datesmodel in datesdb)
            {
                Dates.Add(datesmodel);
            }

        }

        void ShowAllOverDates()
        {
            OverDates.Clear();
            var overdb = db_overdate.GetItems<DatesModel>();
            foreach (var overmodel in overdb)
            {
                OverDates.Add(overmodel);
            }
        }

        void Calculations(bool calc)
        {
            ShowAllRecords();
            if (calc == true)
            {
                PercentCalc();
                Prediction();
                MinLine();
                Telemetry();
                //Omethod();
            }

        }

        
        void ClearForm()
        {
            date = DateTime.Today;
            dt_ab = string.Empty;
            dt_tot = string.Empty;

            RaisePropertyChanged(nameof(date));
            RaisePropertyChanged(nameof(dt_ab));
            RaisePropertyChanged(nameof(dt_tot));

        }



    }
}