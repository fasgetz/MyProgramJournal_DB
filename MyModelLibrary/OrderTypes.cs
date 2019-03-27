using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class OrderTypes
    {
        public OrderTypes()
        {
            this.OrderArchive = new List<OrderArchive>();
        }

        public int IdOrderType { get; set; }
        public string OrderName { get; set; }

        public List<OrderArchive> OrderArchive { get; set; }
    }
}
