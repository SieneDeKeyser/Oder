using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using Oder.Services.Items;
using Microsoft.Extensions.Logging;
using Oder.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Oder.Domain.Items.Exceptions;

namespace Oder.Api.UnitTests.Items
{
   public class ItemsControllerTests
    {
        private readonly IItemService _itemServiceStub;
        private readonly ILogger<ItemsController> _itemLoggerStub;
        private ItemsController _itemsController;

        public ItemsControllerTests()
        {
            _itemServiceStub = Substitute.For<IItemService>();
            _itemLoggerStub = Substitute.For<ILogger<ItemsController>>();
            _itemsController = new ItemsController(_itemServiceStub, _itemLoggerStub);
        }

        [Fact]
        public void GivenNewItemWithNameDescriptionPriceAndStockAmountAsAdministrator_WhenCreatingNewItem_ThenReturnStatusCode201WithItemDTO()
        {
            //Given
            ItemDTO itemDto1 = new ItemDTO() { Name = "testItem", Description = "test description", AmountInStock = 5, Price = 10 };
            _itemServiceStub.CreateNewItem(itemDto1).Returns(itemDto1);

            //When
            CreatedResult result = (CreatedResult) _itemsController.CreateNewItem(itemDto1).Result;

            //Then
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(itemDto1, result.Value);
        }

        [Fact]
        public void GivenNewItemWithoutNameAsAdministrator_WhenCreatingNewItem_ThenReturnStatusCode201WithItemDTO()
        {
            //Given
            ItemDTO itemDto1 = new ItemDTO() {Description = "test description", AmountInStock = 5, Price = 10 };
            _itemServiceStub.CreateNewItem(itemDto1).Returns(ex => { throw new ItemInputException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult)_itemsController.CreateNewItem(itemDto1).Result;

            //Then
            Assert.Equal("Please provide all fields required for this Item", result.Value);
        }

        [Fact]
        public void GivenNewItemWithAllPropertiesAsAdministratorAndExistingItemId_WhenUpdatingItem_ThenReturnOK()
        {
            //Given
            ItemDTO itemDto1 = new ItemDTO() { Name = "testItem", Description = "test description", AmountInStock = 5, Price = 10 };
            _itemServiceStub.UpdateItem(0, itemDto1).Returns(itemDto1);

            //When
            OkObjectResult result = (OkObjectResult)_itemsController.UpdateItem(itemDto1, 0).Result;

            //Then
            Assert.Equal(itemDto1, result.Value);
        }
    }
}
