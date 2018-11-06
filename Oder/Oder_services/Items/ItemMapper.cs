using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Items;

namespace Oder.Services.Items
{
    public class ItemMapper : IItemMapper
    {
        public Item FromItemDTOToItem(ItemDTO itemDTO)
        {
            return new Item()
            {
                Description = itemDTO.Description,
                Price = itemDTO.Price,
                AmountInStock = itemDTO.AmountInStock,
                Name = itemDTO.Name
            };
        }

        public ItemDTO FromItemToItemDTO(Item item)
        {
            return new ItemDTO()
            {
                Description = item.Description,
                Price = item.Price,
                AmountInStock = item.AmountInStock,
                Name = item.Name,
                Id = item.Id
            };
        }
    }
}
