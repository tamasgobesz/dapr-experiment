using System;

namespace OrdersApp.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public string OrderData { get; set; }
    }
}
