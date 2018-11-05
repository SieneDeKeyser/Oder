using Oder.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.ItemGroups
{
   public class ItemGroupService
    {
        private IItemRepository _itemRepository;
        public ItemGroupService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
    }
}
