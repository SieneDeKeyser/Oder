using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Oder.Services.Customers;
using Oder.Domain.Adresses;
using Newtonsoft.Json;

namespace Oder.IntegrationTests.Customers
{
   public class CustomersIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CustomersIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task GivenNewsCustomerWithAllPropertiesAndWithoutId_WhenCreatingNewCustomer_ThenSuccessStatusCode()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";
            var customerJsonObject = JsonConvert.SerializeObject(customerDTO);
            var stringContent = new StringContent(customerJsonObject, Encoding.UTF8, "application/json");

            //When
            var response = await _client.PostAsync("api/customers", stringContent);

            //then
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GivenNewsCustomerWithAllPropertiesAndWithId_WhenCreatingNewCustomer_ThenReturnNoSuccessStatusCode()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Id = 0;
            customerDTO.Firstname = "test";
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";
            var customerJsonObject = JsonConvert.SerializeObject(customerDTO);
            var stringContent = new StringContent(customerJsonObject, Encoding.UTF8, "application/json");

            //When
            var response = await _client.PostAsync("api/customers", stringContent);

            //then
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GivenNewsCustomerWithoutFirstName_WhenCreatingNewCustomer_ThenReturnNoSuccessStatusCode()
        {
            //Given
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";
            var customerJsonObject = JsonConvert.SerializeObject(customerDTO);
            var stringContent = new StringContent(customerJsonObject, Encoding.UTF8, "application/json");

            //When
            var response = await _client.PostAsync("api/customers", stringContent);

            //then
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}
