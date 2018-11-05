using Oder.Domain.Items;
using Oder.Domain.Orders;
using Oder.Services.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IItemGroupMapper _itemGroupMapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;

        public OrderService(IItemGroupMapper itemGroupMapper, IOrderRepository orderRepository, IItemRepository itemRepository)
        {
            _itemGroupMapper = itemGroupMapper;
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
        }
        public OrderDTO CreateNewOrder(OrderDTO newOrderDTO)
        {
            throw new NotImplementedException();
        }

        private
    }
}
