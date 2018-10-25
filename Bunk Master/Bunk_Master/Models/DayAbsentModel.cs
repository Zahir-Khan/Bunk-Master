using System;
using System.Collections.Generic;
using System.Text;

namespace Bunk_Master
{
    public class DayAbsentModel
    {
        public int Id { get; set; } 
        public int Rdata { get; set; }

        public override string ToString()
        {
            return $"{Id},{Rdata}";
        }
    }
}
