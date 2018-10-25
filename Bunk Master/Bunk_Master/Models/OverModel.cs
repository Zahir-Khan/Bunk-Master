using System;
using System.Collections.Generic;
using System.Text;

namespace Bunk_Master
{
    public class OverModel : BaseItem
    {
        public int presentnos { get; set; }
        public int totnos { get; set; }
        public int mincutoff { get; set; }
        public string repeat_data { get; set; }

        public override string ToString()
        {
            return $"{ID},{presentnos},{totnos},{mincutoff},{repeat_data}";
        }
    }

}
