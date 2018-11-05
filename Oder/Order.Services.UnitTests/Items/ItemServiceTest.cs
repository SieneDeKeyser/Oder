using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using Oder.Services.Items;
using Oder.Domain.Items;
using Oder.Domain.Items.Exceptions;

namespace Order.Services.UnitTests.Items
{
   public class ItemServiceTest
    {
        private IItemMapper _itemMapper;
        private IItemRepository _itemRepository;
        private IItemService _itemService;

        public ItemServiceTest()
        {
            _itemMapper = Substitute.For<IItemMapper>();
            _itemRepository = Substitute.For<IItemRepository>();
            _itemService = new ItemService(_itemMapper, _itemRepository);
        }

        [Fact]
        public void GivenNewItemDTOWithAllProperties_WhenCreatingNewItem_ThenItemRepositoryReceiveCallToSaveNewItem()
        {
            //Given
            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description"
            };
            Item newItem = new Item()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description"
            };

            _itemMapper.FromItemDTOToItem(newItemDTO).Returns(newItem);

            //When
            _itemService.CreateNewItem(newItemDTO);

            //then
            _itemRepository.Received().SaveNewItemInDB(newItem);
        }

        [Fact]
        public void GivenNewItemDTOWithhoutDescription_WhenCreatingNewItem_ThenThrowItemInputException()
        {
            //Given
            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
            };

            //When
            Action act = () =>_itemService.CreateNewItem(newItemDTO);

            //then
            Assert.Throws<ItemInputException>(act);
        }
    }
}
