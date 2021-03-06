﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers
{
   public interface ICustomerRepository
    {
        void AddNewCustomer(Customer newCustomer);
        Customer GetCustomerById(int id);
        List<Customer> GetAllCustomers();
    }
}
