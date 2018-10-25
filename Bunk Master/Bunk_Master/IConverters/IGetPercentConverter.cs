using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
using System.Linq;

namespace Bunk_Master
{
    class IGetPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "--";

            var str = value.ToString().Split(',').Select(i => i.Trim()).ToList();
            var presentnos = int.Parse(str[1]);
            var totnos = int.Parse(str[2]);
            if (totnos<1)
            {
                return "--";
            }
            else
            {
                double pct = Math.Round(((double)presentnos / totnos) * 100,1);

                return pct.ToString()+"%";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
