using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Messages
{
    public class InventoryCheckResponseMessage
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public bool Succeeded { get; set; }
    }
}
