using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers
{
   public interface ICustomerRepository
    {
        Customer AddNewCustomer(Customer newCustomer);
    }
}
