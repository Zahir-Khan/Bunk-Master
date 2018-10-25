using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace Bunk_Master.Droid
{

    [BroadcastReceiver]
    public class BackgroundReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
            wakeLock.Acquire();

            //Run code here
            Log.Info("AAAPP", "Background Reciever");
            Intent serviceintent = new Intent(context, typeof(PeriodicService));
            context.StartService(serviceintent);

            wakeLock.Release();
        }
    }
}