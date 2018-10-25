using SQLite;
using System.ComponentModel;

namespace Bunk_Master
{
    public class BaseItem  
    {   
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        
        
    }
}
