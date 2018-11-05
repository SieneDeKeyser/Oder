using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers
{
   public class CustomerDB
    {
        public List<Customer> CustomersDB { get; set; }
        public CustomerDB()
        {
            CustomersDB = new List<Customer>();
        }
    }
}
