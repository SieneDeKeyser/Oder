using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Items
{
   public class ItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AmountInStock { get; set; }
        public int Id { get; set; }
        public ItemDTO()
        {
            Id = -1;
        }
    }
}
