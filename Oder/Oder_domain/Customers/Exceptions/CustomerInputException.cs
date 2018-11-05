﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers.Exceptions
{
    public class CustomerInputException : ApplicationException
    {
        public CustomerInputException() : base("You miss some required inputfields for creating a customer")
        {
        }
    }
}