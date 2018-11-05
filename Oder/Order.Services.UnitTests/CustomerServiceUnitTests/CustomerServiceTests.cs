using NSubstitute;
using Oder.Domain.Adresses;
using Oder.Domain.Customers;
using Oder.Domain.Customers.Exceptions;
using Oder.Services.Customers;
using System;
using Xunit;

namespace Order.Services.CustomerServiceUnitTests
{
    public class CustomerServiceUnitTests
    {
        ICustomerRepository customerRepository;
        ICustomerMapper customerMapper;
        CustomerService customerService;
        public CustomerServiceUnitTests()
        {
            customerRepository = Substitute.For<ICustomerRepository>();
            customerMapper = Substitute.For<ICustomerMapper>();
            customerService = new CustomerService(customerRepository, customerMapper);
        }

        [Fact]
        public void GivenNewCustomerDTOWithAllRequiredProperties_WhenCreatingCustomer_ThenCallToCustomerRepositoryWithCustomer()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress()
            {
                PostalCode = 1820,
                City = "Perk",
                Streetname = "Kerstraat",
                HouseNumber = 5
            };
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";

            CustomerBuilder customerbuilder = new CustomerBuilder();
            Customer customer =
                customerbuilder.WithFirstName("test")
                               .WithLastname("test")
                               .WithAddress(
                                    new Adress()
                                    {
                                        PostalCode = 1820,
                                        City = "Perk",
                                        Streetname = "Kerkstraat",
                                        HouseNumber = 5
                                    })
                               .WithPhoneNumber("04/721233456")
                               .WithEmailAdress("xxx@test.com")
                               .Build();
            customerMapper.FromCustomerDTOToCustomer(customerDTO).Returns(customer);

            //When
            customerService.CreateNewCustomer(customerDTO);

            //Then
            customerRepository.Received().AddNewCustomer(customer);
        }

        [Fact]
        public void GivenNewCustomerDTOWithoutFirstName_WhenCreatingCustomer_ThenThrowCustomerException()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress()
            {
                PostalCode = 1820,
                City = "Perk",
                Streetname = "Kerstraat",
                HouseNumber = 5
            };
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";

            //When
            Action act = () => customerService.CreateNewCustomer(customerDTO);

            //Then
            Assert.Throws<CustomerInputException>(act);
        }
    }
}
