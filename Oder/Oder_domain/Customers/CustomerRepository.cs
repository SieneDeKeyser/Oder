using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddNewCustomer(Customer newCustomer)
        {
            CustomersInDataBase.CustomersDB.Add(newCustomer);
        }

        public Customer GetCustomerById(int id)
        {
            return CustomersInDataBase.CustomersDB.SingleOrDefault(customer => customer.Id == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return CustomersInDataBase.CustomersDB;
        }
    }
}
