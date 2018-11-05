using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Items
{
   public class ItemDB
    {
        public List<Item> Items { get; set; }
        public ItemDB()
        {
            Items = new List<Item>();
        }
    }
}
