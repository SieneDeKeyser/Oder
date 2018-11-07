using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oder.Domain.Orders
{
    public class OrderReport
    {
        public int IdOfCustomer { get; set; }
        public List<Order> MyOrders { get; set; }
        public OrderReport()
        {
            MyOrders = new List<Order>();
        }
        public double TotalPriceOfOrder { get; set; }

        private double CalculateTotalPriceOfOrder()
        {
            return MyOrders.Sum(order => order.PriceOfThisOrder);
        }
    }
}
