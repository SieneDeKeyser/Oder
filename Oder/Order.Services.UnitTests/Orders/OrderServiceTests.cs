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
        private IItemRepository _itemRepository;
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;
        private IOrderMapper _orderMapper;
        private OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _itemRepository = Substitute.For<IItemRepository>();
            _customerRepository = Substitute.For<ICustomerRepository>();
            _orderMapper = Substitute.For<IOrderMapper>();

            _itemRepository.GetItemBasedOnId(0)
               .Returns(new Item()
               {
                   Price = 8,
                   AmountInStock = 10,
                   Description = "testDescription1",
                   Name = "TEST1"
               });

            _itemRepository.GetItemBasedOnId(1)
               .Returns(new Item()
               {
                   Price = 2,
                   AmountInStock = 5,
                   Description = "testDescription2",
                   Name = "TEST2"
               });

            _customerRepository.GetCustomerById(0).Returns(new Customer(new CustomerBuilder()));

            _orderService = new OrderService(_orderRepository, _itemRepository, _customerRepository, _orderMapper);
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

            _orderMapper.FromOrderDTOToOrder(newOrderDTO).Returns(newOrder);

            //When
            _orderService.CreateNewOrder(newOrderDTO);

            //then
            _orderRepository.Received().AddNewOrder(newOrder);
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

            _orderMapper.FromOrderDTOToOrder(newOrderDTO).Returns(newOrder);
            _orderMapper.FromOrderToOrderDTO(newOrder).Returns(newOrderDTO);

            //When
            _orderService.CreateNewOrder(newOrderDTO);

            //then
            Assert.Equal(18, newOrder.PriceOfThisOrder);
        }
    }
}
