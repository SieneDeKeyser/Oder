using Oder.Domain.Adresses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oder.Domain.Customers
{
   public class Customer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Adress AdressOfCustomer { get; set; }
        public string PhoneNumber { get; set; }
        public int Id { get;}
        public static int CustomerIdCounter { get; set; }

        public Customer(CustomerBuilder customerBuilder)
        {
            Id = CustomerIdCounter;
            CustomerIdCounter++;
            Firstname = customerBuilder.Firstname;
            Lastname = customerBuilder.Lastname;
            Email = customerBuilder.Email;
            AdressOfCustomer = customerBuilder.Adress;
            PhoneNumber = customerBuilder.PhoneNumber;
        }
    }
}
