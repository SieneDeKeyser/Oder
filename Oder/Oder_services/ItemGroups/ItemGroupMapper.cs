using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.ItemGroups;

namespace Oder.Services.ItemGroups
{
    public class ItemGroupMapper : IItemGroupMapper
    {
        public ItemGroup FromItemGroupDTOToItemGroup(ItemGroupDTO itemGroupDTO)
        {
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
