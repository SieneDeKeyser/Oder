using Oder.Domain.Items;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Order.Domain.UnitTests.Items
{
   public class ItemRepositoryTests
    {
        private ItemRepository _itemRepository;
        public ItemRepositoryTests()
        {
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
    }
}
