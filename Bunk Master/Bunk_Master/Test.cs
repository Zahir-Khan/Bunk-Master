using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;
using Syncfusion.XForms.TabView;
using Syncfusion.ListView.XForms;

namespace Bunk_Master
{
    public class Test :ContentPage
    {
        public Test()
        {
            this.Title = "OVERALL";

            var alay = new AbsoluteLayout() { BackgroundColor = Color.FromHex("#53515b") };
            SfChart chart = new SfChart { BackgroundColor = Color.FromRgba(255, 255, 255, 20) };

        }
    }
}
