using Oder.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Items
{
   public interface IItemMapper
    {
        Item FromItemDTOToItem(ItemDTO itemDTO);
        ItemDTO FromItemToItemDTO(Item item);
    }
}
