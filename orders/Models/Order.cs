using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orders.Models
{
    public enum Status
    {
        OrderGenerated,
        InventoryCheckFailed,
        BillRequested,
        BillingFailed,
        OrderProcessing,
        OrderShipping,
        OrderedProcessed
    }

    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public bool ReceiptGenerated { get; set; }

        public Status Status { get; set; }
    }
}
