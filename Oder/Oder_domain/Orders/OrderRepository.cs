using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Orders
{
    public class OrderRepository : IOrderRepository
    {
        public OrdersDB OrdersInDB { get; set; }
        public OrderRepository()
        {
            OrdersInDB = new OrdersDB();
        }
        public void AddNewOrder(Order order)
        {
            
        }
    }
}
