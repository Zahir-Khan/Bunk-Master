using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Util;
using Xamarin.Forms;
using Android.App.Job;
using Android.Support.Design.Widget;

namespace Bunk_Master.Droid
{
    [Service(Name = "Bunk_Master.PeriodicService")]
    public class PeriodicService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Info("AAAPP", "PeriodicService OnStart");
            MessagingCenter.Send<object>(this, "AutoDB");


            return StartCommandResult.NotSticky;
        }



        }


    [Service(Name = "Bunk_Master.PeriodicJob", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class PeriodicJob : JobService
    {
        public override bool OnStartJob(JobParameters jobParams)
        {
            // Called by the operating system when starting the service.
            // Start up a thread, do work on the thread.
            Android.Util.Log.Info("AAAPP", "Job Scheduler OnStartJob");
            MessagingCenter.Send<object>(this, "AutoDB");
            return true;
        }

        public override bool OnStopJob(JobParameters jobParams)
        {
            // Called by Android when it has to terminate a running service.
            Android.Util.Log.Info("AAAPP", "Job Scheduler OnStopJob");
            return false; // Don't reschedule the job.
        }
    }
}