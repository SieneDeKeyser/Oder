using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.ItemGroups
{
   public class ItemGroupDTO
    {
        /*itemGroupmapper moet checken indien je een itemgroup meegeeft als user
         dus check dit in de ItemGroupMapper en schrijf er testen voor
             */
        public int ItemId { get; set; }
        public int AmountOfThisItem { get; set; }
        public DateTime? ShippingDate { get; set; }
        public double TotalPriceItemGroup { get; set; }

        public ItemGroupDTO()
        {
            ShippingDate = null;
            TotalPriceItemGroup = 0;
        }
    }
}
