using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class OrderArchive
    {
        public int IdOrder { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Commentary { get; set; }
        public int IdOrderType { get; set; }

        public OrderTypes OrderTypes { get; set; }
    }
}
