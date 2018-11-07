using Oder.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders
{
   public interface IOrderReportMapper
    {
        OrderReportDTO FromOrderReportToOrderReportDTO(List<Order> orderReportToMap);
    }
}
