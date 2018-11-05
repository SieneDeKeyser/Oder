using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerDB CustomersInDataBase { get; set; }
        public CustomerRepository()
        {
            CustomersInDataBase = new CustomerDB();
        }
        public Customer AddNewCustomer(Customer newCustomer)
        {
            CustomersInDataBase.CustomersDB.Add(newCustomer);
            return newCustomer;
        }
    }
}
