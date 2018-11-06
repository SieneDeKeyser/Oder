using NSubstitute;
using Oder.Domain.Items;
using Oder.Domain.Orders.ItemGroups;
using Oder.Domain.Orders;
using Xunit;
using Oder.Domain.Customers;

namespace Oder.Domain.UnitTests.Orders
{
    public class OrderRepositoryTests
    {
        private OrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            _orderRepository = new OrderRepository();
        }


        [Fact]
        public void GivenNewOrder_WhenCreateNewOrder_ThenOrderInDB()
        {
            //Given
            ItemGroup itemgroup1 = new ItemGroup() { ItemId = 0, AmountOfThisItem = 2, PriceOfItem=5 };
            ItemGroup itemGroup2 = new ItemGroup() { ItemId = 1, AmountOfThisItem = 1 , PriceOfItem=8};
            Order newOrder = new Order() {IdOfCustomer  = 0};
            newOrder.ItemGroups.Add(itemgroup1);
            newOrder.ItemGroups.Add(itemGroup2);

            //When
            _orderRepository.AddNewOrder(newOrder);

            //Then
            Assert.Contains(newOrder, _orderRepository.OrdersInDB.Orders);
        }

        [Fact]
        public void GivenNewOrder_WhenCalculatePriceOfOrder_ThenReturnCorrectPrice()
        {
            //Given
            ItemGroup itemgroup1 = new ItemGroup() { ItemId = 0, AmountOfThisItem = 2, PriceOfItem=5 };
            ItemGroup itemGroup2 = new ItemGroup() { ItemId = 1, AmountOfThisItem = 1, PriceOfItem=8 };
            Order newOrder = new Order() { IdOfCustomer = 0 };
            newOrder.ItemGroups.Add(itemgroup1);
            newOrder.ItemGroups.Add(itemGroup2);
            _orderRepository.AddNewOrder(newOrder);

            //When
            double actualPrice = newOrder.CalculatePriceOfOrder();

            //Then
            Assert.Equal(18, actualPrice);
        }
    }
}
