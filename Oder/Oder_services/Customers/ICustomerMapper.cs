using Oder.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Customers
{
   public interface ICustomerMapper
    {
        CustomerDTO FromCustomerToCustomerDTO(Customer customer);
        Customer FromCustomerDTOToCustomer(CustomerDTO customerDTO);
    }
}
