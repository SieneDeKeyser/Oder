using Oder.Domain.Customers;
using Oder.Domain.Customers.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerMapper _customerMapper;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository, ICustomerMapper customerMapper)
        {
            _customerMapper = customerMapper;
            _customerRepository = customerRepository;
        }

        public CustomerDTO CreateNewCustomer(CustomerDTO newCustomerDTO)
        {
            foreach (var item in newCustomerDTO.GetType().GetProperties())
            {
                if (item.Name != "Id")
                {
                    if ((item.GetValue(newCustomerDTO) == null))
                    {
                        throw new CustomerInputException();
                    }
                }
                else
                {
                    if ((item.GetValue(newCustomerDTO).ToString() != "-1"))
                    {
                        throw new CustomerInputException();
                    }
                }
            }
            Customer newCustomer = _customerMapper.FromCustomerDTOToCustomer(newCustomerDTO);
            _customerRepository.AddNewCustomer(newCustomer);
            return _customerMapper.FromCustomerToCustomerDTO(newCustomer);
        }

    }
}
