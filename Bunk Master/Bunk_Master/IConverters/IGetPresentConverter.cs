using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Xamarin.Forms;
using System.Linq;

namespace Bunk_Master
{

    public class IGetPresentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "--";

            var str = value.ToString().Split(',').Select(i => i.Trim()).ToList();
            var presentnos = int.Parse(str[1]);
            var totnos = int.Parse(str[2]);
            var absentnos = "Absent:  " + (totnos - presentnos).ToString();
            return absentnos;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
