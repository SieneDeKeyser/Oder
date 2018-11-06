using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers.Exceptions
{
    public class CustomerInputException : ApplicationException
    {
        public CustomerInputException() : base("Please provide all fields required for this creating new customer")
        {
        }
    }
}
