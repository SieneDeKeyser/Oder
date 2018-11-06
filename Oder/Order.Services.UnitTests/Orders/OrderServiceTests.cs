using NSubstitute;
using Oder.Domain.Customers;
using Oder.Domain.Items;
using Oder.Domain.Orders;
using Oder.Domain.Orders.ItemGroups;
using Oder.Services.ItemGroups;
using Oder.Services.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Oder.Services.UnitTests.Orders
{
   public class OrderServiceTests
    {
        private IItemRepository _itemRepositoryStub;
        private IOrderRepository _orderRepositoryStub;
        private ICustomerRepository _customerRepositoryStub;
        private IOrderMapper _orderMapperStub;
        private OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryStub = Substitute.For<IOrderRepository>();
            _itemRepositoryStub = Substitute.For<IItemRepository>();
            _customerRepositoryStub = Substitute.For<ICustomerRepository>();
            _orderMapperStub = Substitute.For<IOrderMapper>();

            _itemRepositoryStub.GetItemBasedOnId(0)
               .Returns(new Item()
               {
                   Price = 8,
                   AmountInStock = 10,
                   Description = "testDescription1",
                   Name = "TEST1"
               });

            _itemRepositoryStub.GetItemBasedOnId(1)
               .Returns(new Item()
               {
                   Price = 2,
                   AmountInStock = 5,
                   Description = "testDescription2",
                   Name = "TEST2"
               });

            _customerRepositoryStub.GetCustomerById(0).Returns(new Customer(new CustomerBuilder()));

            _orderService = new OrderService(_orderRepositoryStub, _itemRepositoryStub, _customerRepositoryStub, _orderMapperStub);
        }

        [Fact]
        public void GivenNewOrder_WhenCreatingNewOrder_ThenOrderRepositoryReceieveCallToAddNewOrder()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2};
            ItemGroupDTO itemGroup2DTO = new ItemGroupDTO() { ItemId = 1, AmountOfThisItem = 1};
            OrderDTO newOrderDTO = new OrderDTO() { IdOfCustomer = 0 };
            newOrderDTO.ItemGroupsDTO.Add(itemgroup1DTO);
            newOrderDTO.ItemGroupsDTO.Add(itemGroup2DTO);

            ItemGroup itemgroup1 = new ItemGroup() { ItemId = 0, AmountOfThisItem = 2};
            ItemGroup itemGroup2 = new ItemGroup() { ItemId = 1, AmountOfThisItem = 1,};
            Order newOrder = new Order() { IdOfCustomer = 0 };
            newOrder.ItemGroups.Add(itemgroup1);
            newOrder.ItemGroups.Add(itemGroup2);

            _orderMapperStub.FromOrderDTOToOrder(newOrderDTO).Returns(newOrder);

            //When
            _orderService.CreateNewOrder(newOrderDTO);

            //then
            _orderRepositoryStub.Received().AddNewOrder(newOrder);
        }

        [Fact]
        public void GivenNewOrder_WhenCreatingNewOrder_ThenOrderDTOTOReturnHasCorrectPrice()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2 };
            ItemGroupDTO itemGroup2DTO = new ItemGroupDTO() { ItemId = 1, AmountOfThisItem = 1 };
            OrderDTO newOrderDTO = new OrderDTO() { IdOfCustomer = 0 };
            newOrderDTO.ItemGroupsDTO.Add(itemgroup1DTO);
            newOrderDTO.ItemGroupsDTO.Add(itemGroup2DTO);

            ItemGroup itemgroup1 = new ItemGroup() { ItemId = 0, AmountOfThisItem = 2 };
            ItemGroup itemGroup2 = new ItemGroup() { ItemId = 1, AmountOfThisItem = 1, };
            Order newOrder = new Order() { IdOfCustomer = 0 };
            newOrder.ItemGroups.Add(itemgroup1);
            newOrder.ItemGroups.Add(itemGroup2);

            _orderMapperStub.FromOrderDTOToOrder(newOrderDTO).Returns(newOrder);
            _orderMapperStub.FromOrderToOrderDTO(newOrder).Returns(newOrderDTO);

            //When
            _orderService.CreateNewOrder(newOrderDTO);

            //then
            Assert.Equal(18, newOrder.PriceOfThisOrder);
        }
    }
}
