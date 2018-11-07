using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Items;
using Oder.Domain.Orders.ItemGroups;
using Oder.Domain.Orders.OrderExceptions;

namespace Oder.Services.ItemGroups
{
    public class ItemGroupMapper : IItemGroupMapper
    {
        private IItemRepository _itemRepository;
        public ItemGroupMapper(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
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
                ItemId = itemGroupToMap.ItemId,
                TotalPriceItemGroup = itemGroupToMap.TotalPriceItemGroup
            };
        }

        public ItemGroupDTOForInReport FromItemGroupToItemGroupDTOForInReport(ItemGroup itemGroupToMap)
        {
            Item itemFromGroup = _itemRepository.GetItemBasedOnId(itemGroupToMap.ItemId);
            return new ItemGroupDTOForInReport()
            {
                NameOfItem = itemFromGroup.Name,
                TotalPriceItemGroup = itemGroupToMap.TotalPriceItemGroup,
                OrderedAmount = itemGroupToMap.AmountOfThisItem
            };
        }
    }
}
