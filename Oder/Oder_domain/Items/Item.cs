using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Items
{
   public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AmountInStock { get; set; }
        public static int ItemCounter { get; set; }
        public int Id { get;}
        public Item()
        {
            Id = ItemCounter;
            ItemCounter++;
        }
    }
}
