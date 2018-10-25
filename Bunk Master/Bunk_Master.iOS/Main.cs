using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Syncfusion.SfChart.XForms.iOS.Renderers;

namespace Bunk_Master.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            new SfChartRenderer();
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
