using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Orders.ItemGroups;
using Oder.Domain.Orders;
using Oder.Services.ItemGroups;
using Oder.Domain.Orders.OrderExceptions;

namespace Oder.Services.Orders
{
    public class OrderMapper : IOrderMapper
    {
        private readonly IItemGroupMapper _itemGroupMapper;
        public OrderMapper(IItemGroupMapper itemGroupMapper)
        {
            _itemGroupMapper = itemGroupMapper;
        }
        public OrderDTO FromOrderToOrderDTO(Order orderToMap)
        {
            OrderDTO orderDTOToReturn = new OrderDTO();
            orderDTOToReturn.TotalPrice = orderToMap.PriceOfThisOrder;
            foreach (var itemGroup in orderToMap.ItemGroups)
            {
                ItemGroupDTO itemGroupFromThisOrder = _itemGroupMapper.FromItemGroupToItemGroupDTO(itemGroup);
                orderDTOToReturn.ItemGroupsDTO.Add(itemGroupFromThisOrder);
            }
            orderDTOToReturn.Id = orderToMap.OrderId;
            return orderDTOToReturn;
        }

        public Order FromOrderDTOToOrder(OrderDTO orderDTOToMap)
        {
            Order orderToReturn = new Order();
            if (orderDTOToMap.IdOfCustomer == -1 || orderDTOToMap.ItemGroupsDTO.Count == 0)
            {
                throw new OrderException();
            }
            foreach (var itemgroupDTO in orderDTOToMap.ItemGroupsDTO)
            {
                ItemGroup itemGroupFromThisOrder = _itemGroupMapper.FromItemGroupDTOToItemGroup(itemgroupDTO);
                orderToReturn.ItemGroups.Add(itemGroupFromThisOrder);
            }
            orderToReturn.CalculatePriceOfOrder();
            return orderToReturn;
        }
    }
}
