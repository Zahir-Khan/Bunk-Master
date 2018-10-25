using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Bunk_Master
{
    public class Scheduler
    {
        public Scheduler()
        {
            MessagingCenter.Subscribe<object>(this, "AutoDB", (sender) =>
            {
                AutoDB();
            });

        }

        public void AutoDB()
        {
            if (DateTime.Today - ViewModel.diagnosticsVMInstance.GetdbUpdateDate() > TimeSpan.FromDays(.9)) //Check if a day has passed
            {
                var list = ViewModel.ClassVMInstance.Records.ToList();

                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var rData = list[i].repeat_data;
                    var id = (int)list[i].ID;
                    if (rData == "0.0.0.0.0.0.0")
                    {
                        //Do Nothing
                    }
                    else
                    {
                        var rData_day = rData.Split('.').ToList();
                        var tot = rData_day[((int)DateTime.Now.DayOfWeek + 6) % 7];
                        ViewModel.ClassVMInstance.DatesVMInstance.Sched(id, tot);
                    }
                }
                ViewModel.diagnosticsVMInstance.UpdatedbUpdateDate(); // Update the update date
            }
        }
    }
}