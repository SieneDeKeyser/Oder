using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Customers
{
   public interface ICustomerService
    {
        CustomerDTO CreateNewCustomer(CustomerDTO newCustomerDTO);
        List<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomerById(int id);
    }
}
