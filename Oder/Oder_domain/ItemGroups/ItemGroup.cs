using Oder.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.ItemGroups
{
    public class ItemGroup
    {
        private readonly IItemRepository _itemRepository;
        public int ItemId { get; set; }
        public int AmountOfThisItem { get; set; }
        public DateTime ShippingDate { get; }
        public double TotalPriceItemGroup { get; }

        public ItemGroup(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            ShippingDate = CalculateShippingDate();
            TotalPriceItemGroup = CalculateTotalPrice();
        }

        private DateTime CalculateShippingDate()
        {
            DateTime shipdate = new DateTime();
            Item itemOfThisGroup = _itemRepository.GetItemBasedOnId(ItemId);
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

        private double CalculateTotalPrice()
        {
            Item itemOfThisGroup = _itemRepository.GetItemBasedOnId(ItemId);
            double price = -1;
            price = itemOfThisGroup.Price * AmountOfThisItem;
            return price;
        }
    }
}
