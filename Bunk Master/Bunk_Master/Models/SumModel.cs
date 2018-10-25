using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace Bunk_Master
{
    class SumModel : BaseItem
    {
        public int Sum_ab { get; set; }
        public int Sum_tot { get; set; }

        public override string ToString()
        {
            return $"{Sum_ab},{Sum_tot}";
        }
    }
}
