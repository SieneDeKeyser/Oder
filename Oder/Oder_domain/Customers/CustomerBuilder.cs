using Oder.Domain.Adresses;
using Oder.Domain.Customers.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Oder.Domain.Customers
{
    public class CustomerBuilder
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Adress Adress { get; set; }
        public string PhoneNumber { get; set; }

        public static CustomerBuilder CreateCustomerBuilder()
        {
            return new CustomerBuilder();
        }

        public Customer Build()
        {
            return new Customer(this);
        }

        public CustomerBuilder WithFirstName(string firstname)
        {
            Firstname = firstname;
            return this;
        }

        public CustomerBuilder WithLastname(string lastname)
        {
            Lastname = lastname;
            return this;
        }

        public CustomerBuilder WithAddress(Adress adress)
        {
            Adress = adress;
            return this;
        }

        public CustomerBuilder WithEmailAdress(string email)
        {
            if (IsMailAdressValid(email))
            {
                Email = email;
                return this;
            }
            throw new CustomerInputException();
        }

        public CustomerBuilder WithPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        private bool IsMailAdressValid(string email)
        {

            try
            {
                MailAddress mailadressToCheck = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
