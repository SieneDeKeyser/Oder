using Oder.Domain.Customers;
using Oder.Domain.Orders.ItemGroups;
using Oder.Domain.Items;
using Oder.Domain.Orders;
using Oder.Services.ItemGroups;
using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Orders.OrderExceptions;
using System.Linq;

namespace Oder.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderMapper _orderMapper;

        public OrderService(IOrderRepository orderRepository,
                            IItemRepository itemRepository,
                            ICustomerRepository customerRepository,
                            IOrderMapper orderMapper)
        {
            _itemRepository = itemRepository;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderMapper = orderMapper;
        }


        public OrderDTO CreateNewOrder(OrderDTO newOrderDTO)
        {
            Order newOrder = _orderMapper.FromOrderDTOToOrder(newOrderDTO);
            
            newOrder.CustomerOfThisOrder = SearchCustomer(newOrderDTO.IdOfCustomer);
            if (newOrder.CustomerOfThisOrder == null)
            {
                throw new OrderException();
            }
            foreach (var itemGroup in newOrder.ItemGroups)
            {
                Item itemOfThisGroup = _itemRepository.GetItemBasedOnId(itemGroup.ItemId);
                itemGroup.PriceOfItem = itemOfThisGroup.Price;
                itemGroup.ShippingDate = itemGroup.CalculateShippingDate(itemOfThisGroup);
                itemOfThisGroup.AmountInStock -= itemGroup.AmountOfThisItem;
            }
            _orderRepository.AddNewOrder(newOrder);
            return _orderMapper.FromOrderToOrderDTO(newOrder);
        }

        public List<OrderDTO> GetAllOrders()
        {
            return _orderRepository.GetAllOrders().Select(order => { return _orderMapper.FromOrderToOrderDTO(order); }).ToList();
        }

        private Customer SearchCustomer(int idCustomer)
        {
            Customer customerOfThisOrder = _customerRepository.GetCustomerById(idCustomer);
            return customerOfThisOrder;
        }
    }
}
