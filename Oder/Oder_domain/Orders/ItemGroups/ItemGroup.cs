using Oder.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Orders.ItemGroups
{
    public class ItemGroup
    {
        public int ItemId { get; set; }
        public int AmountOfThisItem { get; set; }
        public DateTime ShippingDate { get; set; }
        public double PriceOfItem { get; set; }
        public double TotalPriceItemGroup { get { return CalculateTotalPriceOfItemGroup(); } }

        public ItemGroup()
        {

        }

        public double CalculateTotalPriceOfItemGroup()
        {     
            double price = -1;
            price = PriceOfItem * AmountOfThisItem;
            return price;
        }

        public DateTime CalculateShippingDate(Item itemOfThisGroup)
        {
            DateTime shipdate = new DateTime();
            if (itemOfThisGroup.AmountInStock >= AmountOfThisItem)
            {
                shipdate = DateTime.Today.AddDays(1);
            }
            else
            {
                shipdate = DateTime.Today.AddDays(7);
            }
            return shipdate;
        }
    }
}
