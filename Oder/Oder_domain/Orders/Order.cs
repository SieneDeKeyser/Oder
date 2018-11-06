using Oder.Domain.Customers;
using Oder.Domain.Orders.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Orders
{
   public class Order
    {
        
        public List<ItemGroup> ItemGroups { get; set; }
        public int IdOfCustomer { get; set; }
        public double PriceOfThisOrder { get { return CalculatePriceOfOrder(); } }
        public Customer CustomerOfThisOrder { get; set; }
        public int OrderId { get;}
        public static int OrderCounter { get; set; }
        public Order()
        {
            ItemGroups = new List<ItemGroup>();
            OrderId = OrderCounter;
            OrderCounter++;
        }

        public double CalculatePriceOfOrder()
        {
            double price = 0;
            foreach (var itemgroup in ItemGroups)
            {
                price += itemgroup.CalculateTotalPriceOfItemGroup();
            }
            return price;
        }
    }
}
