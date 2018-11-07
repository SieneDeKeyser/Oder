using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Orders;
using Oder.Services.ItemGroups;

namespace Oder.Services.Orders
{
    public class OrderReportMapper : IOrderReportMapper
    {
        private IItemGroupMapper _itemGroupMapper;

        public OrderReportMapper(IItemGroupMapper itemGroupMapper)
        {
            _itemGroupMapper = itemGroupMapper;
        }


        public OrderReportDTO FromOrderReportToOrderReportDTO(List<Order> orderReportToMap)
        {
            OrderReportDTO orderReportToReturn = new OrderReportDTO();
            foreach (var order in orderReportToMap)
            {
                OrderDTOForInReport orderDTOforReport = new OrderDTOForInReport()
                {
                    Id = order.OrderId,
                    TotalPrice = order.PriceOfThisOrder
                };

                foreach (var itemGroup in order.ItemGroups)
                {
                    orderDTOforReport.ItemGroupsDTO.Add(_itemGroupMapper.FromItemGroupToItemGroupDTOForInReport(itemGroup));
                }
                orderReportToReturn.MyOrders.Add(orderDTOforReport);
            }

            return orderReportToReturn;
        }
    }
}
