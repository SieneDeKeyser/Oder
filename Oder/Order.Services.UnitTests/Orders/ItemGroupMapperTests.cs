using Oder.Services.ItemGroups;
using Oder.Services.Orders.OrderExceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Oder.Services.UnitTests.Orders
{
   public class ItemGroupMapperTests
    {
        private ItemGroupMapper _itemMapper;
        public ItemGroupMapperTests()
        {
            _itemMapper = new ItemGroupMapper();
        }

        [Fact]
        public void GivenItemGroupDTOWithOnlyItemIdAndAmount_WhenMappingToNewItemGroup_ThenMappingIsOk()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2 };

            //When
            var actualResult = _itemMapper.FromItemGroupDTOToItemGroup(itemgroup1DTO);

            //Then
            Assert.NotNull(actualResult);
        }

        [Fact]
        public void GivenItemGroupDTOWithTotalPrice_WhenMappingToNewItemGroup_ThenOrderExceptionIsThrown()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2, TotalPriceItemGroup = 5 };

            //When
            Action act = () => _itemMapper.FromItemGroupDTOToItemGroup(itemgroup1DTO);

            //Then
            Assert.Throws<OrderException>(act);
        }


        [Fact]
        public void GivenItemGroupDTOWithShippingDate_WhenMappingToNewItemGroup_ThenOrderExceptionIsThrown()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2, ShippingDate = new DateTime() };

            //When
            Action act = () => _itemMapper.FromItemGroupDTOToItemGroup(itemgroup1DTO);

            //Then
            Assert.Throws<OrderException>(act);
        }

        [Fact]
        public void GivenItemGroupDTOWithShippingDateAndTotalPrice_WhenMappingToNewItemGroup_ThenOrderExceptionIsThrown()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2, ShippingDate = new DateTime(), TotalPriceItemGroup=1 };

            //When
            Action act = () => _itemMapper.FromItemGroupDTOToItemGroup(itemgroup1DTO);

            //Then
            Assert.Throws<OrderException>(act);
        }
    }
}
