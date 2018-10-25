using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Bunk_Master
{
    
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU0MzBAMzEzNjJlMzMyZTMwQkVqdEZMdTZFNStNOFEvbVQ5SU9XY1kyOEJuWkVWa0Zoc0k1WXV0b3FpRT0=");
            InitializeComponent();
            
            MainPage = new NavigationPage(new HomeTabPage()) { BarBackgroundColor=Color.FromHex("#53515b") };

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
