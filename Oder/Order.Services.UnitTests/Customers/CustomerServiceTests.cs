using NSubstitute;
using Oder.Domain.Adresses;
using Oder.Domain.Customers;
using Oder.Domain.Customers.Exceptions;
using System.Collections;
using Oder.Services.Customers;
using System;
using Xunit;
using System.Collections.Generic;

namespace Oder.Services.Customers
{
    public class CustomerServiceUnitTests
    {
        ICustomerRepository _customerRepositoryStub;
        ICustomerMapper _customerMapperStub;
        CustomerService _customerService;
        public CustomerServiceUnitTests()
        {
            _customerRepositoryStub = Substitute.For<ICustomerRepository>();
            _customerMapperStub = Substitute.For<ICustomerMapper>();
            _customerService = new CustomerService(_customerRepositoryStub, _customerMapperStub);
        }

        [Fact]
        public void GivenNewCustomerDTOWithAllRequiredProperties_WhenCreatingCustomer_ThenCallToCustomerRepositoryWithCustomer()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";

            CustomerBuilder customerbuilder = new CustomerBuilder();
            Customer customer =
                customerbuilder.WithFirstName("test")
                               .WithLastname("test")
                               .WithAddress(
                                    new Adress(1820, "Perk", "kerkstraat", 5))
                               .WithPhoneNumber("04/721233456")
                               .WithEmailAdress("xxx@test.com")
                               .Build();
            _customerMapperStub.FromCustomerDTOToCustomer(customerDTO).Returns(customer);

            //When
            _customerService.CreateNewCustomer(customerDTO);

            //Then
            _customerRepositoryStub.Received().AddNewCustomer(customer);
        }

        [Fact]
        public void GivenNewCustomerDTOWithoutFirstName_WhenCreatingCustomer_ThenThrowCustomerException()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";

            //When
            Action act = () => _customerService.CreateNewCustomer(customerDTO);

            //Then
            Assert.Throws<CustomerInputException>(act);
        }

        [Fact]
        public void GivenNewCustomerDTOWithAllNeededPropertiesAndId_WhenCreatingCustomer_ThenThrowCustomerException()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Id = 0;
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";

            //When
            Action act = () => _customerService.CreateNewCustomer(customerDTO);

            //Then
            Assert.Throws<CustomerInputException>(act);
        }

        [Fact]
        public void GivenCustomerId_WhenGetCustomerById_ThenCallToCustomerRepositoryWithThisId()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";

            CustomerBuilder customerbuilder = new CustomerBuilder();
            Customer customer =
                customerbuilder.WithFirstName("test")
                               .WithLastname("test")
                               .WithAddress(
                                    new Adress(1820, "Perk", "kerkstraat", 5))
                               .WithPhoneNumber("04/721233456")
                               .WithEmailAdress("xxx@test.com")
                               .Build();
            _customerMapperStub.FromCustomerToCustomerDTO(customer).Returns(customerDTO);
            _customerRepositoryStub.GetCustomerById(customer.Id).Returns(customer);

            //When
            _customerService.GetCustomerById(customer.Id);

            //Then
            _customerRepositoryStub.Received().GetCustomerById(customer.Id);
        }


        [Fact]
        public void GivenNonExistingCustomerId_WhenGetCustomerById_ThenThrowCustomerNotFoundException()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Id = 0;
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";

            //When
            Action act = () => _customerService.GetCustomerById(-1);

            //Then
            Assert.Throws<CustomerNotFoundException>(act);
        }
    }
}
