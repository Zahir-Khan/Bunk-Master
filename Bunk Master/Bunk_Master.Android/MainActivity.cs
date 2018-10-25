using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Util;
using Java.Lang;
using Acr.UserDialogs;
using Android.App.Job;
using Android.Support.Design.Widget;
using Xamarin.Forms;
using Android.Util;
using Lottie.Forms.Droid;

namespace Bunk_Master.Droid
{
    [Activity(Label = "Bunk_Master", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //Rg.Plugins.popup
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            //Lottie
            //AnimationViewRenderer.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //Acr UserDialogue
            UserDialogs.Init(this);

            LoadApplication(new App());

            //Background Stuff
            if (Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
            {
                //var demoServiceIntent = new Intent("Bunk_Master.PeriodicService");
                //var demoServiceConnection = new ServiceConnection(this);
                //BindService(demoServiceIntent, demoServiceConnection, Bind.AutoCreate);

                var alarmIntent = new Intent(this, typeof(BackgroundReciever));

                if (PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.NoCreate) == null)
                {

                    var pending = PendingIntent.GetBroadcast(this, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

                    var alarmManager = GetSystemService(AlarmService).JavaCast<AlarmManager>();

                    Calendar setTime = Calendar.Instance;
                    setTime.TimeInMillis = JavaSystem.CurrentTimeMillis();
                    setTime.Set(CalendarField.HourOfDay, 14);
                    setTime.Set(CalendarField.Minute, 05);

                    alarmManager.SetInexactRepeating(AlarmType.RtcWakeup, setTime.TimeInMillis, AlarmManager.IntervalDay, pending);
                }
            }

            else
            {



                // Sample usage - creates a JobBuilder for a DownloadJob andsets the Job ID to 1.
                var jobBuilder = this.CreateJobBuilderUsingJobId<PeriodicJob>(1);
                
                jobBuilder.SetPeriodic(60000);
                jobBuilder.SetPersisted(true);
                var jobInfo = jobBuilder.Build();  // creats a JobInfo object.

                var jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);
                var scheduleResult = jobScheduler.Schedule(jobInfo);

                if (JobScheduler.ResultSuccess == scheduleResult)
                {
                    Android.Util.Log.Info("AAAPP", "Job Scheduled Succesfully");
                }
                else
                {
                    Android.Util.Log.Info("AAAPP", "Job Schedule Failed");

                }
            }
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
    }
}

