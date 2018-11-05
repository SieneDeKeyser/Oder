using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Items;
using Oder.Domain.Items.Exceptions;

namespace Oder.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly IItemMapper _itemMapper;
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemMapper itemMapper, IItemRepository itemRepository)
        {
            _itemMapper = itemMapper;
            _itemRepository = itemRepository;
        }
        public ItemDTO CreateNewItem(ItemDTO itemDTO)
        {
            foreach (var item in itemDTO.GetType().GetProperties())
            {
                if ((item.GetValue(itemDTO) == null))
                {
                    throw new ItemInputException();
                }
            }
            Item newItem = _itemMapper.FromItemDTOToItem(itemDTO);
            _itemRepository.SaveNewItemInDB(newItem);
            return _itemMapper.FromItemToItemDTO(newItem);
        }
    }
}
