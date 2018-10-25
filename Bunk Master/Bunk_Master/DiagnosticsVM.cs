using System;
using System.Collections.Generic;
using System.Text;

namespace Bunk_Master
{
    public class DiagnosticsVM : BaseVM
    {
        readonly Database db_diag;

        public DateTime Lastdbupdate { get; set; }


        public DiagnosticsVM()
        {
            db_diag = new Database("settings");
            db_diag.CreateTable<SettingModel>();
            Initialize();
        }

        public void Initialize()
        {
            //Check At Startup if db has default values
            var t = db_diag.Query<SettingModel>("SELECT * FROM SettingModel WHERE ID = 1", new object[] { });
            if (t.Count == 0)
            {
                var settingmodel = new SettingModel
                {
                    Lastdbupdate = DateTime.Today
                };
                db_diag.SaveItem(settingmodel);
                ViewModel.ClassVMInstance.AddSampleData(); //should run only once after install
            }
        }

        public void UpdatedbUpdateDate()
        {
            db_diag.ExecuteQuery("UPDATE SettingModel SET Lastdbupdate = ? WHERE ID = 1", new object[]{ DateTime.Today });
        }

        public DateTime GetdbUpdateDate()
        {
            var t = db_diag.Query<SettingModel>("SELECT * FROM SettingModel WHERE ID = 1", new object[] { });
            if (t.Count==0)
            {
                return DateTime.MinValue.Date;
            }
            else
            {
                return DateTime.Parse(t[0].ToString().Split(',')[1]).Date;
            }
        }
    }
}
