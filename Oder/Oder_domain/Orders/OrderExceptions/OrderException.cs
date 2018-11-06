using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Orders.OrderExceptions
{
   public class OrderException : ApplicationException
    {
        public OrderException() : base("This order cannot be processed")
        {
        }
    }
}
