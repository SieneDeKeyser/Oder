using Oder.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Items
{
   public interface IItemService
    {
        ItemDTO CreateNewItem(ItemDTO itemDTO);
        List<ItemDTO> GetAllItems();
        ItemDTO GetItemById(int id);
        ItemDTO UpdateItem(int id, ItemDTO itemToUpdateDTO);
    }
}
