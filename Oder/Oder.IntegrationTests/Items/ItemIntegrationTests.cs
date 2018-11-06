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

namespace Oder.IntegrationTests.Items
{
    public class ItemIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ItemIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async void GivenNewItemWithAllPropertiesAndWithoutIdAsAdmin_WhenCreatingNewItem_ThenReturnStatusCodeSuccess()
        {
            //Given
            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Description = "Test description"
            };

            var customerJsonObject = JsonConvert.SerializeObject(newItemDTO);
            var stringContent = new StringContent(customerJsonObject, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");

            //When
            var response = await _client.PostAsync("api/items", stringContent);

            //then
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void GivenNewItemWithAllPropertiesAndWithIdAsAdmin_WhenCreatingNewItem_ThenReturnStatusCodeNotSuccess()
        {
            //Given
            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Name = "Test",
                Id = 0,
                Description = "Test description"
            };

            var customerJsonObject = JsonConvert.SerializeObject(newItemDTO);
            var stringContent = new StringContent(customerJsonObject, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");

            //When
            var response = await _client.PostAsync("api/items", stringContent);

            //then
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void GivenNewItemWithoutNameAsAdmin_WhenCreatingNewItem_ThenReturnStatusCodeNotSuccess()
        {
            //Given
            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Description = "Test description"
            };

            var ItemJsonObject = JsonConvert.SerializeObject(newItemDTO);
            var stringContent = new StringContent(ItemJsonObject, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");

            //When
            var response = await _client.PostAsync("api/items", stringContent);

            //then
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void GivenItemAndExistingItemIDAdmin_WhenUpdatingItem_ThenReturnStatusCodeSuccess()
        {
            //Given
            ItemDTO newItemDTO = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 10.0,
                Description = "Test description",
                Name = "test"
            };

            var ItemJsonObject1 = JsonConvert.SerializeObject(newItemDTO);
            var stringContent1 = new StringContent(ItemJsonObject1, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");
            var response1 = _client.PostAsync("api/items", stringContent1).Result;
            ItemDTO newItemDTO2 =  (ItemDTO) response1.Content.ReadAsAsync(typeof(ItemDTO)).Result;

            ItemDTO newItemDTOForUpdate = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 5.0,
                Description = "Test description",
                Name = "testXX"
            };

            //When
            var ItemJsonObject2 = JsonConvert.SerializeObject(newItemDTOForUpdate);
            var stringContent2 = new StringContent(ItemJsonObject2, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");
            var response2 = await _client.PutAsync($"api/items/{newItemDTO2.Id}", stringContent2);

            //then
            Assert.True(response2.IsSuccessStatusCode);
        }

        [Fact]
        public async void GivenItemAndNonExistingItemIDAdmin_WhenUpdatingItem_ThenReturnStatusCodeSuccess()
        {

            //Given
            ItemDTO newItemDTOForUpdate = new ItemDTO()
            {
                AmountInStock = 5,
                Price = 5.0,
                Description = "Test description",
                Name = "testXX"
            };

            //When
            var ItemJsonObject2 = JsonConvert.SerializeObject(newItemDTOForUpdate);
            var stringContent2 = new StringContent(ItemJsonObject2, Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = CreateBasicHeader("Admin", "AdminPassword");
            var response2 = await _client.PutAsync("api/items/-1", stringContent2);

            //then
            Assert.False(response2.IsSuccessStatusCode);
        }

        private AuthenticationHeaderValue CreateBasicHeader(string username, string password)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(username + ":" + password);
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }
    }
    
}
