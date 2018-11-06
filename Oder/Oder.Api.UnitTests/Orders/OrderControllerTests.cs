using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using Oder.Services.Orders;
using Microsoft.Extensions.Logging;
using Oder.Api.Controllers;
using Oder.Domain.Orders.ItemGroups;
using Oder.Services.ItemGroups;
using Microsoft.AspNetCore.Mvc;
using Oder.Domain.Orders.OrderExceptions;

namespace Oder.Api.UnitTests.Orders
{
   public class OrderControllerTests
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _orderLogger;
        private readonly OrdersController _ordersController;

        public OrderControllerTests()
        {
            _orderService = Substitute.For<IOrderService>();
            _orderLogger = Substitute.For<ILogger<OrdersController>>();
            _ordersController = new OrdersController(_orderService, _orderLogger);
        }

        [Fact]
        public void GivenNewOrderDTOWithItemGroupWithItemIdAndAmount_WhenMakeNewOrder_ThenOrderServiceReceiveCallWithThisOrderDTO()
        {
            //Given
            ItemGroupDTO itemgroupDTO1 = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2 };
            OrderDTO orderDTO1 = new OrderDTO() { IdOfCustomer = 0 };
            orderDTO1.ItemGroupsDTO.Add(itemgroupDTO1);
            _orderService.CreateNewOrder(orderDTO1).Returns(orderDTO1);

            //When
            _ordersController.MakeNewOrder(orderDTO1);

            //Then
            _orderService.Received().CreateNewOrder(orderDTO1);
        }

        [Fact]
        public void GivenNewOrderDTOWithItemGroupWithItemIdAndAmount_WhenMakeNewOrder_ThenReturnOKWithOrderDTO()
        {
            //Given
            ItemGroupDTO itemgroupDTO1 = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2 };
            OrderDTO orderDTO1 = new OrderDTO() { IdOfCustomer = 0 };
            orderDTO1.ItemGroupsDTO.Add(itemgroupDTO1);
            _orderService.CreateNewOrder(orderDTO1).Returns(orderDTO1);

            //When
            CreatedResult result =  (CreatedResult) _ordersController.MakeNewOrder(orderDTO1).Result;

            //Then
            Assert.Equal(orderDTO1, result.Value);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void GivenNewOrderDTOWithItemGroupWithItemIdAndAmountAndShippingDate_WhenMakeNewOrder_ThenReturnBadRequestWithOrderExceptionMessage()
        {
            //Given
            ItemGroupDTO itemgroupDTO1 = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2, ShippingDate = new DateTime() };
            OrderDTO orderDTO1 = new OrderDTO() { IdOfCustomer = 0 };
            orderDTO1.ItemGroupsDTO.Add(itemgroupDTO1);
            _orderService.CreateNewOrder(orderDTO1).Returns(ex => { throw new OrderException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult) _ordersController.MakeNewOrder(orderDTO1).Result;

            //Then
            Assert.Equal("This order cannot be processed", result.Value);
        }

        [Fact]
        public void GivenNewOrderDTOWithItemGroupWithItemIdAndAmountAndPrice_WhenMakeNewOrder_ThenReturnBadRequestWithOrderExceptionMessage()
        {
            //Given
            ItemGroupDTO itemgroupDTO1 = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2, TotalPriceItemGroup = 5};
            OrderDTO orderDTO1 = new OrderDTO() { IdOfCustomer = 0 };
            orderDTO1.ItemGroupsDTO.Add(itemgroupDTO1);
            _orderService.CreateNewOrder(orderDTO1).Returns(ex => { throw new OrderException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult)_ordersController.MakeNewOrder(orderDTO1).Result;

            //Then
            Assert.Equal("This order cannot be processed", result.Value);
        }

        [Fact]
        public void GivenNewOrderDTOWithItemGroupWithItemIdAndAmountAndPriceAndShippingDate_WhenMakeNewOrder_ThenReturnBadRequestWithOrderExceptionMessage()
        {
            //Given
            ItemGroupDTO itemgroupDTO1 = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2, TotalPriceItemGroup = 5, ShippingDate = new DateTime() };
            OrderDTO orderDTO1 = new OrderDTO() { IdOfCustomer = 0 };
            orderDTO1.ItemGroupsDTO.Add(itemgroupDTO1);
            _orderService.CreateNewOrder(orderDTO1).Returns(ex => { throw new OrderException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult)_ordersController.MakeNewOrder(orderDTO1).Result;

            //Then
            Assert.Equal("This order cannot be processed", result.Value);
        }
    }
}
