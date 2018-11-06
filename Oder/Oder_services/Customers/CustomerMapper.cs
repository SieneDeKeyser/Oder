using System;
using System.Collections.Generic;
using System.Text;
using Oder.Domain.Customers;

namespace Oder.Services.Customers
{
    public class CustomerMapper : ICustomerMapper
    {
        public Customer FromCustomerDTOToCustomer(CustomerDTO customerDTO)
        {
            return new CustomerBuilder().WithFirstName(customerDTO.Firstname)
                                        .WithLastname(customerDTO.Lastname)
                                        .WithAddress(customerDTO.AdressOfCustomer)
                                        .WithPhoneNumber(customerDTO.PhoneNumber)
                                        .WithEmailAdress(customerDTO.Email)
                                        .Build();
        }

        public CustomerDTO FromCustomerToCustomerDTO(Customer customer)
        {
            return new CustomerDTO()
            {
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                Id = customer.Id
            };
        }
    }
}
