using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Orders.OrderExceptions
{
   public class OrderException : ApplicationException
    {
        public OrderException() : base("This order cannot be processed")
        {
        }
    }
}
