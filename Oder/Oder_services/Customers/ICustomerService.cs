using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Customers
{
   public interface ICustomerService
    {
        CustomerDTO CreateNewCustomer(CustomerDTO newCustomerDTO);
    }
}
