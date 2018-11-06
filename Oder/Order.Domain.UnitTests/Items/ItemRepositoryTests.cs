using Oder.Domain.Items;
using Oder.Domain.Items.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Oder.Domain.UnitTests.Items
{
   public class ItemRepositoryTests
    {
        private ItemRepository _itemRepository;
        public ItemRepositoryTests()
        {
            Item.ItemCounter = 0;
            _itemRepository = new ItemRepository();
        }
        [Fact]
        public void GivenNewItem_WhenSaveNewItem_ThenItemInItemsDB()
        {

            //Given
            Item newItem = new Item();
            newItem.Name = "testItem";
            newItem.Description = "test description";
            newItem.Price = 2.0;
            newItem.AmountInStock = 5;

            //when
            _itemRepository.SaveNewItemInDB(newItem);

            //then
            Assert.Contains(newItem, _itemRepository.itemsInDB.Items);
        }

        [Fact]
        public void GivenItemIdExistingItem_WhenUpdatingItem_ThenItemInItemsDBUpdated()
        {

            //Given
            Item newItem = new Item();
            newItem.Name = "testItem";
            newItem.Description = "test description";
            newItem.Price = 2.0;
            newItem.AmountInStock = 5;
            _itemRepository.SaveNewItemInDB(newItem);

            Item UpdateItem = new Item();
            UpdateItem.Name = "testItem";
            UpdateItem.Description = "test description";
            UpdateItem.Price = 8.0;
            UpdateItem.AmountInStock = 5;

            //when
            _itemRepository.UpdateItem(0, UpdateItem);

            //then
            Assert.Equal(0, UpdateItem.Id);
            Assert.Equal(8.0, UpdateItem.Price);
            Assert.Single(_itemRepository.itemsInDB.Items);
        }

        [Fact]
        public void GivenItemIdNonExistingItem_WhenUpdatingItem_ThenThrowItemNotFoundException()
        {
            //Given
            Item newItem = new Item();
            newItem.Name = "testItem";
            newItem.Description = "test description";
            newItem.Price = 2.0;
            newItem.AmountInStock = 5;
            _itemRepository.SaveNewItemInDB(newItem);

            Item UpdateItem = new Item();
            UpdateItem.Name = "testItem";
            UpdateItem.Description = "test description";
            UpdateItem.Price = 8.0;
            UpdateItem.AmountInStock = 5;

            //when
            Action act = () => _itemRepository.UpdateItem(1, UpdateItem);

            //then
            Assert.Throws<ItemNotFoundException>(act);
        }
    }
}
