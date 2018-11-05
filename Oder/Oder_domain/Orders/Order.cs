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
        public double PriceOfThisOrder { get; private set; }
        public Customer CustomerOfThisOrder { get; set; }
        public Order()
        {
            ItemGroups = new List<ItemGroup>();
        }

        public void CalculatePriceOfOrder()
        {
            double price = 0;
            foreach (var itemgroup in ItemGroups)
            {
                price += itemgroup.TotalPriceItemGroup;
            }
            PriceOfThisOrder = price;
        }
    }
}
