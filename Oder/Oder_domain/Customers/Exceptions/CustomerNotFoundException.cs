using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers.Exceptions
{
    public class CustomerNotFoundException : ApplicationException
    {
        public CustomerNotFoundException() : base("Customer with this id does not exist")
        {
        }
    }
}
