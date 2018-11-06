using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Orders.ItemGroups;
using Oder.Services.Orders.OrderExceptions;

namespace Oder.Services.ItemGroups
{
    public class ItemGroupMapper : IItemGroupMapper
    {
        public ItemGroup FromItemGroupDTOToItemGroup(ItemGroupDTO itemGroupDTO)
        {
            if (itemGroupDTO.TotalPriceItemGroup != 0 || itemGroupDTO.ShippingDate != null)
            {
                throw new OrderException();
            }
            return new ItemGroup()
            {
                AmountOfThisItem = itemGroupDTO.AmountOfThisItem,
                ItemId = itemGroupDTO.ItemId,
            };
        }

        public ItemGroupDTO FromItemGroupToItemGroupDTO(ItemGroup itemGroupToMap)
        {
            return new ItemGroupDTO()
            {
                AmountOfThisItem = itemGroupToMap.AmountOfThisItem,
                ShippingDate = itemGroupToMap.ShippingDate,
                ItemId = itemGroupToMap.ItemId
            };
        }
    }
}
