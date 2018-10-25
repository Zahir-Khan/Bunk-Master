using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace Bunk_Master
{
     public class DatesModel: BaseItem
    {
        [Indexed]
        public DateTime date { get; set; }
        public int dt_ab { get; set; }
        public int dt_tot { get; set; }

        public override string ToString()
        {
            return $"{ID},{date},{dt_ab},{dt_tot}"; 
        }

    }
}