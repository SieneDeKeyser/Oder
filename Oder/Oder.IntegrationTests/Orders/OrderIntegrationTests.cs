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
using Oder.Services.Items;
using System;
using Oder.Services.ItemGroups;
using Oder.Services.Orders;
using Oder.Domain.Customers;
using Oder.Domain.Orders;
using Oder.Domain.Items;

namespace Oder.IntegrationTests.Orders
{
   public class OrderIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public OrderIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            Customer.CustomerIdCounter = 0;
            Order.OrderCounter = 0;
            Item.ItemCounter = 0;
            PrepareItemsAndCustomers();
        }


        [Fact]
        public async void GivenNewOrderWithCustomerIdWithItemGroupWithoutShippingDatePriceWithAmountAndItemId_WhenCreatingMakingNewOrder_ThenReturnStatusCodeSuccess()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2 };
            ItemGroupDTO itemGroup2DTO = new ItemGroupDTO() { ItemId = 1, AmountOfThisItem = 1 };
            OrderDTO newOrderDTO = new OrderDTO() { IdOfCustomer = 0 };
            newOrderDTO.ItemGroupsDTO.Add(itemgroup1DTO);
            newOrderDTO.ItemGroupsDTO.Add(itemGroup2DTO);

            //When
            var OrderJsonObject = JsonConvert.SerializeObject(newOrderDTO);
            var stringContentOrder = new StringContent(OrderJsonObject, Encoding.UTF8, "application/json");
            var responseOrder = await _client.PostAsync("api/orders", stringContentOrder);

            //then
            Assert.True(responseOrder.IsSuccessStatusCode);
        }

        [Fact]
        public async void GivenNewOrderWithoutCustomerIdWithItemGroupWithoutShippingDatePriceWithAmountAndItemId_WhenCreatingMakingNewOrder_ThenReturnStatusCodeNotSuccess()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2 };
            ItemGroupDTO itemGroup2DTO = new ItemGroupDTO() { ItemId = 1, AmountOfThisItem = 1 };
            OrderDTO newOrderDTO = new OrderDTO();
            newOrderDTO.ItemGroupsDTO.Add(itemgroup1DTO);
            newOrderDTO.ItemGroupsDTO.Add(itemGroup2DTO);

            //When
            var OrderJsonObject = JsonConvert.SerializeObject(newOrderDTO);
            var stringContentOrder = new StringContent(OrderJsonObject, Encoding.UTF8, "application/json");
            var responseOrder = await _client.PostAsync("api/orders", stringContentOrder);

            //then
            Assert.False(responseOrder.IsSuccessStatusCode);
        }

        [Fact]
        public async void GivenNewOrderWithItemGroupWithShippingDateWithoutPriceWithAmountAndItemId_WhenCreatingMakingNewOrder_ThenReturnStatusCodeNotSuccess()
        {
            //Given
            ItemGroupDTO itemgroup1DTO = new ItemGroupDTO() { ItemId = 0, AmountOfThisItem = 2, ShippingDate = new DateTime() };
            ItemGroupDTO itemGroup2DTO = new ItemGroupDTO() { ItemId = 1, AmountOfThisItem = 1 };
            OrderDTO newOrderDTO = new OrderDTO() { IdOfCustomer = 0 };
            newOrderDTO.ItemGroupsDTO.Add(itemgroup1DTO);
            newOrderDTO.ItemGroupsDTO.Add(itemGroup2DTO);

            //When
            var OrderJsonObject = JsonConvert.SerializeObject(newOrderDTO);
            var stringContentOrder = new StringContent(OrderJsonObject, Encoding.UTF8, "application/json");
            var responseOrder = await _client.PostAsync("api/orders", stringContentOrder);

            //then
            Assert.False(responseOrder.IsSuccessStatusCode);
        }

        private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(username + ":" + password);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        private async void PrepareItemsAndCustomers()
        {
            ItemDTO newItemDTO1 = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test1",
                Description = "Test description1"
            };

            ItemDTO newItemDTO2 = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test2",
                Description = "Test description2"
            };

            var ItemJsonObject1 = JsonConvert.SerializeObject(newItemDTO1);
            var stringContent1 = new StringContent(ItemJsonObject1, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");
            var response1 = await _client.PostAsync("api/items", stringContent1);

            var ItemJsonObject2 = JsonConvert.SerializeObject(newItemDTO2);
            var stringContent2 = new StringContent(ItemJsonObject2, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");
            var response2 = await _client.PostAsync("api/items", stringContent1);

            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.Firstname = "test";
            customerDTO.Lastname = "test";
            customerDTO.AdressOfCustomer = new Adress(1820, "Perk", "kerkstraat", 5);
            customerDTO.Email = "xxx@test.com";
            customerDTO.PhoneNumber = "04/72123456";
            var customerJsonObject = JsonConvert.SerializeObject(customerDTO);
            var stringContentCustomer = new StringContent(customerJsonObject, Encoding.UTF8, "application/json");
            var responseCustomer = await _client.PostAsync("api/customers", stringContentCustomer);
        }
    }
}
