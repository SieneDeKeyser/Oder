using Oder.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Items
{
   public interface IItemRepository
    {
        void SaveNewItemInDB(Item item);
        Item GetItemBasedOnId(int id);
        List<Item> GetItems();
        Item UpdateItem(int id, Item itemToUpdate);
    }
}
