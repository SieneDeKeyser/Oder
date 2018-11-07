using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders
{
   public class OrderReportDTO
    {
        public List<OrderDTOForInReport> MyOrders { get; set; }
        public double TotalPriceOfOrder { get; set; }

        public OrderReportDTO()
        {
            MyOrders = new List<OrderDTOForInReport>();
        }


    }
}
