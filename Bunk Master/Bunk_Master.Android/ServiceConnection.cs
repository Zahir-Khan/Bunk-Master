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
using Android.Util;

namespace Bunk_Master.Droid
{
    public class ServiceConnection : Java.Lang.Object, IServiceConnection
    {

        MainActivity mainActivity;
        public ServiceConnection(MainActivity activity)
        {
            IsConnected = false;
            Binder = null;
            mainActivity = activity;
        }

        public bool IsConnected { get; private set; }
        public CustomServiceBinder Binder { get; private set; }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            Binder = service as CustomServiceBinder;
            IsConnected = this.Binder != null;

            if (IsConnected)
            {
                Log.Info("AAAPP", "Bound To Service");
            }
            else
            {
                Log.Info("AAAPP", "Not Bound To Service");
            }

        }

        public void OnServiceDisconnected(ComponentName name)
        {
            Log.Info("AAAPP", "Service Disconnected");
            IsConnected = false;
            Binder = null;
        }

    }
}
