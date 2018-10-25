using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Globalization;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SkiaSharp;

namespace Bunk_Master
{
    public class ClassVM : BaseVM
    {
        private int monday = 0;
        private int tuesday = 0;
        private int wednesday = 0;
        private int thursday = 0;
        private int friday = 0;
        private int saturday = 0;
        private int sunday = 0;

        readonly Database db0;
        readonly Database db1;

        public string classname { get; set; }
        public string presentnos { get; set; }
        public string totnos { get; set; }
        public string mincutoff { get; set; } = "75";
        public string repeat_data { get; set; } = "0.0.0.0.0.0.0";
        public DateTime startdate { get; set; } = DateTime.Today;
        public SKColor color { get; set; } = SKColor.Parse("ffa6a5ff");
        #region Days Properties

        public int Monday
        {
            get { return monday; }
            set
            {
                monday = value;
                RaisePropertyChanged(nameof(Monday));
            }
        }
        public int Tuesday
        {
            get { return tuesday; }
            set
            {
                tuesday = value;
                RaisePropertyChanged(nameof(Tuesday));
            }
        }
        public int Wednesday
        {
            get { return wednesday; }
            set
            {
                wednesday = value;
                RaisePropertyChanged(nameof(Wednesday));
            }
        }
        public int Thursday
        {
            get { return thursday; }
            set
            {
                thursday = value;
                RaisePropertyChanged(nameof(Thursday));
            }
        }
        public int Friday
        {
            get { return friday; }
            set
            {
                friday = value;
                RaisePropertyChanged(nameof(Friday));
            }
        }
        public int Saturday
        {
            get { return saturday; }
            set
            {
                saturday = value;
                RaisePropertyChanged(nameof(Saturday));
            }
        }
        public int Sunday
        {
            get { return sunday; }
            set
            {
                sunday = value;
                RaisePropertyChanged(nameof(Sunday));
            }
        }
        #endregion
        public string Overpres { get; set; } = "00";
        public string Overtot { get; set; } = "00";
        public string Overab { get; set; } = "00";
        public string Overperc { get; set; } = "--%";
        public string Overmincut { get; set; } = "75";
        public string Overrepeat { get; set; } = "0.0.0.0.0.0.0";

        public DatesVM DatesVMInstance { get; } = new DatesVM();

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        //public ICommand DeleteAllCommand { get; set; }
        public ICommand UpdateNameCommand { get; set; }

        public ObservableCollection<ClassModel> Records { get; set; }


        public ClassVM()
        {
            AddCommand = new Command(Add);
            DeleteCommand = new Command(Delete);
            //DeleteAllCommand = new Command(DeleteAll);
            UpdateNameCommand = new Command(UpdateName);

            db0 = new Database("overalldb");
            db0.CreateTable<OverModel>();
            db1 = new Database("classdb");
            db1.CreateTable<ClassModel>();

            Records = new ObservableCollection<ClassModel>();
            ShowAllRecords();
        }


        public void CurrentForm(object obj)  //NOT USED  //This code is good but similar code exist below so need to make it more concise
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            presentnos = columns[1];
            totnos = columns[2];
            classname = columns[3];
            mincutoff = columns[4];
            repeat_data = columns[5];
            startdate = DateTime.Parse(columns[6]);
            SKColor.TryParse(columns[7], out var tmp);
            color = tmp;
        }

        public List<DayAbsentModel> GetItemIds() //Wtith Rdata
        {
            var query = db1.Query<ClassModel>("SELECT * FROM ClassModel", new object[] { });
            var itemlist = new List<DayAbsentModel>();
            foreach (var item in query)
            {
                var model = new DayAbsentModel
                {
                    Id = int.Parse(item.ToString().Split(',').ToList()[0]),
                    Rdata = int.Parse(item.ToString().Split(',').ToList()[5].Split('.').ToList()[((int)DateTime.Today.DayOfWeek + 6) % 7])
                };
                itemlist.Add(model);
            }
            return itemlist;
        }

        public void Add()
        {
            int presentnos_t = 0;
            int totnos_t = 0;
            int mincutoff_t = 0;
            repeat_data = $"{monday}.{tuesday}.{wednesday}.{thursday}.{friday}.{saturday}.{sunday}";
            List<int> rlist = new List<int> { monday, tuesday, wednesday, thursday, friday, saturday, sunday };

            if ((int.TryParse(presentnos, out presentnos_t)) && (int.TryParse(totnos, out totnos_t)) && (int.TryParse(mincutoff, out mincutoff_t)))
            {
                var record = new ClassModel
                {
                    classname = Regex.Replace(classname, "[^a-zA-Z0-9 ]+", "").ToUpper(),
                    presentnos = presentnos_t,
                    totnos = totnos_t,
                    mincutoff = mincutoff_t,
                    repeat_data = repeat_data,
                    startdate = startdate,
                    color = color.ToString()

                };

                UpdateOverModel(presentnos_t, totnos_t);
                UpdateOverModelRepeat(rlist);
                db1.SaveItem(record);
                var id_inter = db1.GetLastInsertrowid();
                DatesVMInstance.NewDB(id_inter);
                Records.Add(record);
                RaisePropertyChanged(nameof(Records));
                ClearForm();
            }
            ShowAllRecords();
        }

        void Delete(object obj)
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            if (int.TryParse(columns[0], out int id))
            {
                var items = GetRow(id);
                UpdateOverModel(-int.Parse(items[1]), -int.Parse(items[2]));
                UpdateOverModelRepeat(items[5].Split('.').Select(i => -int.Parse(i.Trim())).ToList());
                db1.DeleteItem<ClassModel>(id);
                DatesVMInstance.DeleteDB(id);
                ShowAllRecords();
            }
        }

        public void DayAbsent()
        {

        }

        void DeleteAll() //Not Integrated with overall db
        {
            db1.DeleteAll<ClassModel>();
            Records.Clear();
        }

        void UpdateOverModel(int pres, int tot)
        {
            if (db0.GetItems<OverModel>().Count() == 0)
            {
                var defaultOverModel = new OverModel
                {
                    mincutoff = 75,
                    repeat_data = "0.0.0.0.0.0.0",
                    presentnos = 0,
                    totnos = 0
                };
                db0.SaveItem(defaultOverModel);
            }

            db0.ExecuteQuery("UPDATE OverModel SET presentnos = presentnos + ? , totnos = totnos + ? WHERE ID = 1", new object[] { pres, tot });
            ShowAllOverData();
        }

        void UpdateOverModelRepeat(List<int> repeatData)
        {
            var res = db0.Query<OverModel>("SELECT * FROM OverModel WHERE ID = 1", new object[] { });

            var columns = res[0].ToString().Split(',').Select(i => (i.Trim())).ToList();
            var rdata = columns[4].ToString().Split('.').Select(i => double.Parse(i.Trim())).ToList();

            for (int i = 0; i < 7; i++)
            {
                rdata[i] += repeatData[i];
            }
            db0.ExecuteQuery("UPDATE OverModel SET repeat_data = ? WHERE ID = 1", new object[] { $"{rdata[0]}.{rdata[1]}.{rdata[2]}.{rdata[3]}.{rdata[4]}.{rdata[5]}.{rdata[6]}" });
            ShowAllOverData();
        }

        public void UpdateOverModelMincut(int mincut)
        {
            db0.ExecuteQuery("UPDATE OverModel SET mincutoff = ? WHERE ID = 1", new object[] { mincut });
            ShowAllOverData();
            ViewModel.ClassVMInstance.DatesVMInstance.Omethod();
        }

        void UpdateName(object obj)
        {
            int presentnos_t;
            int totnos_t;
            int mincutoff_t;
            int id;


            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            string repeatdata_t = columns[5];

            if (int.TryParse(columns[0], out id) &&
                    (int.TryParse(columns[1], out presentnos_t)) &&
                    (int.TryParse(columns[2], out totnos_t)) &&
                    (int.TryParse(columns[4], out mincutoff_t)))
            {
                var record = new ClassModel
                {
                    ID = id,
                    classname = classname,
                    totnos = totnos_t,
                    presentnos = presentnos_t,
                    mincutoff = mincutoff_t,
                    repeat_data = repeatdata_t,
                    startdate = startdate,
                    color = color.ToString()
                };
                db1.SaveItem(record);

                ClearForm();
            }
            ShowAllRecords();

        }

        public void UpdateRepeatData(int id)
        {
            repeat_data = $"{monday}.{tuesday}.{wednesday}.{thursday}.{friday}.{saturday}.{sunday}";
            db1.ExecuteQuery("UPDATE ClassModel SET repeat_data = ? WHERE ID = ?", new object[] { repeat_data, id });
            UpdateOverModelRepeat(new List<int> { monday, tuesday, wednesday, thursday, friday, saturday, sunday });
            ShowAllRecords();
        }

        public void UpdateNos(int id, int pnos, int tot)
        {
            db1.ExecuteQuery("UPDATE ClassModel SET presentnos = presentnos + ? , totnos = totnos + ? WHERE ID = ?", new object[] { pnos, tot, id });
            UpdateOverModel(pnos, tot);
            ShowAllRecords();
        }

        public void UpdateStartDate(int id, DateTime dt, DateTime olddt)
        {

            if (dt < olddt)
            {
                db1.ExecuteQuery("UPDATE ClassModel SET startdate = ? WHERE ID = ?", new object[] { dt, id });
                ShowAllRecords();
            }
            else if (dt > olddt)
            {
                DatesVMInstance.DeleteDtRange(olddt, dt.AddDays(-1), id);
                db1.ExecuteQuery("UPDATE ClassModel SET startdate = ? WHERE ID = ?", new object[] { dt, id });
                ShowAllRecords();
            }
            //Reflect startdate changes in minline
            DatesVMInstance.MinLine();
            DatesVMInstance.PercentCalc();
        }

        public List<string> GetRow(int id)
        {
            var res = db1.Query<ClassModel>("SELECT * FROM ClassModel WHERE ID = ?", new object[] { id });
            var itemString = res[0].ToString();
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            return columns;
        }

        public OverModel GetOverdb()
        {
            var res = db0.GetItems<OverModel>();
            return res.FirstOrDefault();
        }

        public void ClearForm()
        {
            classname = string.Empty;
            presentnos = string.Empty;
            totnos = string.Empty;
            mincutoff = "75";
            repeat_data = "0.0.0.0.0.0.0";
            startdate = DateTime.Today;
            color = SKColors.LightSteelBlue;
            Monday = 0;
            Tuesday = 0;
            Wednesday = 0;
            Thursday = 0;
            Friday = 0;
            Saturday = 0;
            Sunday = 0;

            RaisePropertyChanged(nameof(classname));
            RaisePropertyChanged(nameof(presentnos));
            RaisePropertyChanged(nameof(totnos));
            RaisePropertyChanged(nameof(mincutoff));
            RaisePropertyChanged(nameof(repeat_data));
            RaisePropertyChanged(nameof(startdate));
            RaisePropertyChanged(nameof(color));

        }

        public void AddSampleData() //should run only once after install
        {
            classname = "Sample";
            presentnos = "7";
            totnos = "9";
            mincutoff = "75";
            monday = 2;
            wednesday = 1;
            thursday = 1;
            saturday = 1;
            startdate = DateTime.Today;
            color = new SKColor(96, 130, 182);
            Add();
            ClearForm();
        }

        void ShowAllRecords()
        {
            Records.Clear();
            var classdb = db1.GetItems<ClassModel>();

            foreach (var classmodel in classdb)
            {
                Records.Add(classmodel);
            }

            ShowAllOverData();

        }

        void ShowAllOverData()
        {
            var overdb = db0.GetItems<OverModel>();
            if (overdb.Count() > 0)
            {
                var p = overdb.First().presentnos;
                var t = overdb.First().totnos;
                double pct = Math.Round(((double)p / t) * 100, 1);
                Overpres = p.ToString();
                Overtot = t.ToString();
                Overab = (t - p).ToString();
                Overperc = pct.ToString() + "%";
                Overmincut = overdb.First().mincutoff.ToString();
                Overrepeat = overdb.First().repeat_data.ToString();
                RaisePropertyChanged(nameof(Overpres));
                RaisePropertyChanged(nameof(Overtot));
                RaisePropertyChanged(nameof(Overab));
                RaisePropertyChanged(nameof(Overperc));
                RaisePropertyChanged(nameof(Overmincut));
                RaisePropertyChanged(nameof(Overrepeat));
            }
        }
    }
}
