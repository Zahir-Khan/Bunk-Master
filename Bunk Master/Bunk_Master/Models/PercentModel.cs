using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunk_Master
{
    public class PercentModel : BaseItem
    {
        public double gpercent { get; set; }
        public DateTime gdate { get; set; }

        public override string ToString()
        {
            return $"{gpercent},{gdate}";
        }
    }
}
