using Oder.Domain.Customers;
using Oder.Domain.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Orders
{
   public class Order
    {
        
        public List<ItemGroup> ItemGroups { get; set; }
        public int IdOfCustomer { get; set; }
        public double PriceOfThisOrder { get;}
        public Customer CustomerOfThisOrder { get; set; }
        public Order()
        {
            ItemGroups = new List<ItemGroup>();
            PriceOfThisOrder = CalculatePriceOfOrder();
        }

        private double CalculatePriceOfOrder()
        {
            double price = 0;
            foreach (var itemgroup in ItemGroups)
            {
                price += itemgroup.TotalPriceItemGroup;
            }
            return price;
        }
    }
}
