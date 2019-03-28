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
        public OrderArchive(int IdOrder, Nullable<System.DateTime> Date, string Commentary, int IdOrderType, OrderTypes OrderTypes)
        {
            this.IdOrder = IdOrder;
            this.Date = Date;
            this.Commentary = Commentary;
            this.IdOrderType = IdOrderType;
            this.OrderTypes = OrderTypes;
        }

        public int IdOrder { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Commentary { get; set; }
        public int IdOrderType { get; set; }

        public OrderTypes OrderTypes { get; set; }
    }
}
