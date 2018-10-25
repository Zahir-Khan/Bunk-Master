using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bunk_Master
{
    public class ClassModel : BaseItem
    {
        public string classname { get; set; }
        public int presentnos{ get; set; } 
        public int totnos { get; set; }
        public int mincutoff { get; set; }
        public string repeat_data { get; set; }
        public DateTime startdate { get; set; }
        public string color { get; set; }

        public override string ToString()
        {
            return $"{ID},{presentnos},{totnos},{classname},{mincutoff},{repeat_data},{startdate},{color}";      
        }
        
        
    }
}
