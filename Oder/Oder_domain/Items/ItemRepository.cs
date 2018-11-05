using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oder.Domain.Items;

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

        public void SaveNewItemInDB(Item item)
        {
            itemsInDB.Items.Add(item);
        }
    }
}
