using Oder.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders
{
   public interface IOrderMapper
    {
        OrderDTO FromOrderToOrderDTO(Order orderToMap);
        Order FromOrderDTOToOrder(OrderDTO orderDTOToMap);
    }
}
