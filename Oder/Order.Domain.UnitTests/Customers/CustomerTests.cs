using Oder.Domain.Adresses;
using Oder.Domain.Customers;
using Oder.Domain.Customers.Exceptions;
using System;
using Xunit;

namespace Order.Domain.UnitTests.Customers
{
    public class CustomerTests
    {
        [Fact]
        public void GivenAllPropertiesOfCustomer_WhenCreatingCustomer_ThenCustomerIsCreated()
        {
            //Given
            CustomerBuilder customerbuilder = new CustomerBuilder();

            //When
            Customer customer = 
                customerbuilder.WithFirstName("Test")
                               .WithLastname("Test")
                               .WithAddress(
                                    new Adress()
                                    {
                                       PostalCode = 1820,
                                       City = "Perk",
                                       Streetname = "Kerkstraat",
                                       HouseNumber = 5
                                    })
                               .WithPhoneNumber("04/721233456")
                               .WithEmailAdress("test@test.com")
                               .Build();

            //Then
            Assert.Equal("Test", customer.Firstname);
        }

        [Fact]
        public void GivenNotCorrectEmailOfCustomer_WhenCreatingCustomer_ThenCustomerInputExceptionIsTrown()
        {
            //Given
            CustomerBuilder customerbuilder = new CustomerBuilder();

            //When
            Action act = () => customerbuilder.WithEmailAdress("xxx");

            //Then
            Assert.Throws<CustomerInputException>(act);
        }
    }
}
