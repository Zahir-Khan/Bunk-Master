using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Xamarin.Forms;

namespace Bunk_Master
{
   public static class ColorValueConverter
    {
        public static System.Drawing.Color ToSFColor(Xamarin.Forms.Color color)
        {
            return System.Drawing.Color.FromArgb((int)color.A, (int)color.R, (int)color.G, (int)color.B);
            
        }

        public static Xamarin.Forms.Color ToXFColor(System.Drawing.Color color)
        {
            return Xamarin.Forms.Color.FromRgba(color.R, color.G, color.B, color.A);
           


        }
    }

}
