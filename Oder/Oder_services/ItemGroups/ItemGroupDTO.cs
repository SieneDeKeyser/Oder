using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.ItemGroups
{
   public class ItemGroupDTO
    {
        public int ItemId { get; set; }
        public int AmountOfThisItem { get; set; }
        public DateTime ShippingDate { get; set; }
        public double TotalPriceItemGroup { get; set; }

    }
}
