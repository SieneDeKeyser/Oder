using System;
using System.Collections.Generic;
using System.Linq;
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
                if (item.Name != "Id")
                {
                    if ((item.GetValue(itemDTO) == null))
                    {
                        throw new ItemInputException();
                    }
                }
                else
                {
                    if ((item.GetValue(itemDTO).ToString() != "-1"))
                    {
                        throw new ItemInputException();
                    }
                }
            }
            Item newItem = _itemMapper.FromItemDTOToItemWhenCreatingNewItem(itemDTO);
            _itemRepository.SaveNewItemInDB(newItem);
            return _itemMapper.FromItemToItemDTO(newItem);
        }

        public List<ItemDTO> GetAllItems()
        {
            return _itemRepository.GetItems().Select(item => { return _itemMapper.FromItemToItemDTO(item); }).ToList();
        }

        public ItemDTO GetItemById(int id)
        {
            var itemToSearch = _itemRepository.GetItemBasedOnId(id);
            if (itemToSearch == null)
            {
                throw new ItemNotFoundException();
            }
            var itemToReturn = _itemMapper.FromItemToItemDTO(itemToSearch);
            return itemToReturn;
        }

        public ItemDTO UpdateItem(int id, ItemDTO itemToUpdateDTO)
        {
            foreach (var item in itemToUpdateDTO.GetType().GetProperties())
            {
                if (item.Name != "Id")
                {
                    if ((item.GetValue(itemToUpdateDTO) == null))
                    {
                        throw new ItemInputException();
                    }
                }
                else
                {
                    if ((item.GetValue(itemToUpdateDTO).ToString() != "-1"))
                    {
                        throw new ItemInputException();
                    }
                }
            }
            Item ItemToUpdate = _itemMapper.FromItemDTOToItemWhenUpdating(itemToUpdateDTO);
            return _itemMapper.FromItemToItemDTO(_itemRepository.UpdateItem(id, ItemToUpdate));
        }
    }
}
