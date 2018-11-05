using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Orders
{
   public class OrdersDB
    {
        public List<Order> Orders { get; set; }
        public OrdersDB()
        {
            Orders = new List<Order>();
        }
    }
}
