using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using Oder.Services.Items;
using Oder.Domain.Items;
using Oder.Domain.Items.Exceptions;

namespace Oder.Services.UnitTests.Items
{
   public class ItemServiceTest
    {
        private IItemMapper _itemMapperStub;
        private IItemRepository _itemRepositoryStub;
        private IItemService _itemService;

        public ItemServiceTest()
        {
            _itemMapperStub = Substitute.For<IItemMapper>();
            _itemRepositoryStub = Substitute.For<IItemRepository>();
            _itemService = new ItemService(_itemMapperStub, _itemRepositoryStub);
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

            _itemMapperStub.FromItemDTOToItemWhenCreatingNewItem(newItemDTO).Returns(newItem);

            //When
            _itemService.CreateNewItem(newItemDTO);

            //then
            _itemRepositoryStub.Received().SaveNewItemInDB(newItem);
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

        [Fact]
        public void GivenNewItemDTOWithGivenId_WhenCreatingNewItem_ThenThrowItemInputException()
        {
            //Given
            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description",
                Id = 0
            };

            //When
            Action act = () => _itemService.CreateNewItem(newItemDTO);

            //then
            Assert.Throws<ItemInputException>(act);
        }

        [Fact]
        public void GivenNewItemDTOAndExistingID_WhenUpdatingThatItem_ThenCallToItemRepositoryToUpdate()
        {
            //Given
            Item newItem = new Item()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description",
                Id = 0
            };

            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description",
            };

            _itemMapperStub.FromItemDTOToItemWhenUpdating(newItemDTO).Returns(newItem);
            _itemRepositoryStub.UpdateItem(0, newItem).Returns(newItem);
            
            //When
            _itemService.UpdateItem(0, newItemDTO);

            //then
            _itemRepositoryStub.Received().UpdateItem(0, newItem);
        }

        [Fact]
        public void GivenNewItemDTOWithIdAndExistingID_WhenUpdatingThatItem_ThenThrowInputItemException()
        {
            //Given
            Item newItem = new Item()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description",
                Id = 0
            };

            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description",
                Id = 0
            };

            //When
            Action act = () => _itemService.UpdateItem(0, newItemDTO);

            //then
            Assert.Throws<ItemInputException>(act);
        }
    }
}
