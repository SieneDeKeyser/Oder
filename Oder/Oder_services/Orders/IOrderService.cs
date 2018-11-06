using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders
{
   public interface IOrderService
    {
        OrderDTO CreateNewOrder(OrderDTO newOrderDTO);
        List<OrderDTO> GetAllOrders();
    }
}
