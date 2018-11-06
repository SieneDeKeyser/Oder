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
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _customerLogger;
        private CustomersController _customerController;

        public CustomersControllerTests()
        {
            _customerService = Substitute.For<ICustomerService>();
            _customerLogger = Substitute.For<ILogger<CustomersController>>();
            _customerController = new CustomersController(_customerService, _customerLogger);
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
            _customerService.CreateNewCustomer(customerDTO).Returns(customerDTO);

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
            _customerService.CreateNewCustomer(customerDTO).Returns(ex => { throw new CustomerInputException(); });

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
            _customerService.CreateNewCustomer(customerDTO).Returns(ex => { throw new CustomerInputException(); });

            //When
            BadRequestObjectResult result = (BadRequestObjectResult)_customerController.CreateNewCustomer(customerDTO).Result;

            //Then
            Assert.Equal("Please provide all fields required for this creating new customer", result.Value);
        }
    }
}
