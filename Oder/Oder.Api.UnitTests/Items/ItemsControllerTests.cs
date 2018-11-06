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
        private readonly IItemService _itemService;
        private readonly ILogger<ItemsController> _itemLogger;
        private ItemsController _itemsController;

        public ItemsControllerTests()
        {
            _itemService = Substitute.For<IItemService>();
            _itemLogger = Substitute.For<ILogger<ItemsController>>();
            _itemsController = new ItemsController(_itemService, _itemLogger);
        }

        [Fact]
        public void GivenNewItemWithNameDescriptionPriceAndStockAmountAsAdministrator_WhenCreatingNewItem_ThenReturnStatusCode201WithItemDTO()
        {
            //Given
            ItemDTO itemDto1 = new ItemDTO() { Name = "testItem", Description = "test description", AmountInStock = 5, Price = 10 };
            _itemService.CreateNewItem(itemDto1).Returns(itemDto1);

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
            _itemService.CreateNewItem(itemDto1).Returns(ex => { throw new ItemInputException(); });

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
            _itemService.UpdateItem(0, itemDto1).Returns(itemDto1);

            //When
            OkObjectResult result = (OkObjectResult)_itemsController.UpdateItem(itemDto1, 0).Result;

            //Then
            Assert.Equal(itemDto1, result.Value);
        }
    }
}
