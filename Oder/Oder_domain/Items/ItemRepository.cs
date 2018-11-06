using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oder.Domain.Items;
using Oder.Domain.Items.Exceptions;

namespace Oder.Domain.Items
{
    public class ItemRepository : IItemRepository
    {
        public ItemDB itemsInDB;

        public ItemRepository()
        {
            itemsInDB = new ItemDB();
        }

        public Item GetItemBasedOnId(int id)
        {
            return itemsInDB.Items.SingleOrDefault(item => item.Id == id);
        }

        public List<Item> GetItems()
        {
            return itemsInDB.Items;
        }

        public void SaveNewItemInDB(Item item)
        {
            itemsInDB.Items.Add(item);
        }

        public Item UpdateItem(int id, Item itemToUpdate)
        {
            Item item = GetItemBasedOnId(id);
            if (item == null)
            {
                throw new ItemNotFoundException();
            }
            var index = itemsInDB.Items.FindIndex(itemsearch => itemsearch.Id == id);
            itemsInDB.Items[index] = itemToUpdate;
            itemsInDB.Items[index].Id = id;
            return itemsInDB.Items[index];
        }
    }
}
