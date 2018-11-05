using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Orders
{
   public interface IOrderRepository
    {
        void AddNewOrder(Order order);
    }
}
