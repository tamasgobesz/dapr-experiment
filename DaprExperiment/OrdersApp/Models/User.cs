using System;
using System.Collections.Generic;

namespace OrdersApp.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public List<Order> Orders { get; set; }
    }
}
