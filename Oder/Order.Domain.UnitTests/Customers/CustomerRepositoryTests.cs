using Oder.Domain.Adresses;
using Oder.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Oder.Domain.UnitTests.Customers
{
   public class CustomerRepositoryTests
    {
        private readonly CustomerRepository _customerRepository;
        public CustomerRepositoryTests()
        {
            _customerRepository = new CustomerRepository();
        }

        [Fact]
        public void GivenNewUser_WhenCreatingNewUser_ThanUserInDB()
        {
            //Given
            CustomerBuilder customerbuilder = new CustomerBuilder();
            Customer customer =
                customerbuilder.WithFirstName("Test")
                               .WithLastname("Test")
                               .WithAddress(
                                    new Adress(1820, "Perk", "kerkstraat", 5))
                               .WithPhoneNumber("04/721233456")
                               .WithEmailAdress("test@test.com")
                               .Build();

            //When
            _customerRepository.AddNewCustomer(customer);

            //then
            Assert.Contains(customer, _customerRepository.CustomersInDataBase.CustomersDB);
        }

        [Fact]
        public void GivenListOfCustomersInDB_WhenGetAllCustomers_ThReturnList()
        {
            //Given
            CustomerBuilder customerbuilder = new CustomerBuilder();
            Customer customer =
                customerbuilder.WithFirstName("Test")
                               .WithLastname("Test")
                               .WithAddress(
                                    new Adress(1820, "Perk", "kerkstraat", 5))
                               .WithPhoneNumber("04/721233456")
                               .WithEmailAdress("test@test.com")
                               .Build();
            _customerRepository.AddNewCustomer(customer);

            //When
            List<Customer> actualListOfCustomers = _customerRepository.GetAllCustomers();

            //then
            Assert.Contains(customer, actualListOfCustomers);
        }

        [Fact]
        public void GivenListOfCustomersInDB_WhenGetCustomerById_ThenReturnCustomerWithThisId()
        {
            //Given
            CustomerBuilder customerbuilder = new CustomerBuilder();
            Customer customer =
                customerbuilder.WithFirstName("Test")
                               .WithLastname("Test")
                               .WithAddress(
                                    new Adress(1820, "Perk", "kerkstraat", 5))
                               .WithPhoneNumber("04/721233456")
                               .WithEmailAdress("test@test.com")
                               .Build();
            _customerRepository.AddNewCustomer(customer);

            //When
            Customer actualCustomer = _customerRepository.GetCustomerById(customer.Id);

            //then
            Assert.Equal(customer, actualCustomer);
        }
    }
}
