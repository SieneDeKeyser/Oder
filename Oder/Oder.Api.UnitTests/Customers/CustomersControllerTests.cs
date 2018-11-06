using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using Oder.Services.Customers;
using Microsoft.Extensions.Logging;
using Oder.Api.Controllers;
using Oder.Domain.Adresses;
using Microsoft.AspNetCore.Mvc;
using Oder.Domain.Customers.Exceptions;

namespace Oder.Api.UnitTests.Customers
{
   public class CustomersControllerTests
    {
        private readonly ICustomerService _customerServiceStub;
        private readonly ILogger<CustomersController> _customerLoggerStub;
        private CustomersController _customerController;

        public CustomersControllerTests()
        {
            _customerServiceStub = Substitute.For<ICustomerService>();
            _customerLoggerStub = Substitute.For<ILogger<CustomersController>>();
            _customerController = new CustomersController(_customerServiceStub, _customerLoggerStub);
        }

        [Fact]
        public void GivenNewCustomerDTO_WhenCreatingNewCustomer_ThenReturnCreatedWithStatusCode201AndCustomerDTO()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";
            _customerServiceStub.CreateNewCustomer(customerDTO).Returns(customerDTO);

            //When
            CreatedResult result = (CreatedResult) _customerController.CreateNewCustomer(customerDTO).Result;

            //Then
            Assert.Equal(customerDTO, result.Value);
            Assert.Equal(201, result.StatusCode);
        }


        [Fact]
        public void GivenNewCustomerDTOWithoutFirstName_WhenCreatingNewCustomer_ThenReturnBadRequestWithCustomerInputExceptionMessage()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";
            _customerServiceStub.CreateNewCustomer(customerDTO).Returns(ex => { throw new CustomerInputException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult) _customerController.CreateNewCustomer(customerDTO).Result;

            //Then
            Assert.Equal("Please provide all fields required for this creating new customer", result.Value);
        }

        [Fact]
        public void GivenNewCustomerDTOWithId_WhenCreatingNewCustomer_ThenReturnBadRequestWithCustomerInputExceptionMessage()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Id = 0;
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";
            _customerServiceStub.CreateNewCustomer(customerDTO).Returns(ex => { throw new CustomerInputException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult)_customerController.CreateNewCustomer(customerDTO).Result;

            //Then
            Assert.Equal("Please provide all fields required for this creating new customer", result.Value);
        }

        [Fact]
        public void GivenCustomerNonExistingId_WhenCustomerById_ThenReturnBadRequestWithCustomerNotFoundExceptionMessage()
        {
            //Given
            _customerServiceStub.GetCustomerById(-1).Returns(ex => { throw new CustomerNotFoundException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult)_customerController.GetCustomerBasedOnId(-1).Result;

            //Then
            Assert.Equal("Customer with this id does not exist", result.Value);
        }
    }
}
