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

namespace Bunk_Master.Droid
{
    public class CustomServiceBinder : Binder
    {
        public PeriodicService Service { get; private set; }

        public CustomServiceBinder(PeriodicService service)
        {
            this.Service = service;
        }

        public PeriodicService GetPeriodicService()
        {
            return Service;
        }
        
    }
}