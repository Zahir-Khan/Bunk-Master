using System;
using System.Collections.Generic;
using System.Text;

namespace Bunk_Master
{
    public class SettingModel : BaseItem
    {
        public DateTime Lastdbupdate { get; set; }

        public override string ToString()
        {
            return $"{ID},{Lastdbupdate}";
        }
    }

}
