using System;
using System.Collections.Generic;
using System.Net.Http;
using FoodOrderApp;
using FoodOrderApp.Models;
using Newtonsoft.Json;
using Xunit;

namespace FoodOrderApi.Testing.Integration
{
    public class FoodOrderApiTesting
    {
        [Fact]
        public async void PostOrderTest()
        {
            using (var postClient = new HttpClient {BaseAddress = new Uri("http://localhost:8080")})
            {
                var orderToPost = JsonConvert.SerializeObject(
                    new Order("Leah","Hou","Burritos"));

                var postResponse = await postClient.PostAsync("/orders", new StringContent(orderToPost)); //StringContent sets httpRequest header: Content-Type as text/plain which allows the server to interpret the request body as text/plain

                var orderResponse = JsonConvert.DeserializeObject<Order>(await postResponse.Content.ReadAsStringAsync());

                using (var getClient = new HttpClient {BaseAddress = new Uri("http://localhost:8080")})
                {
                    var getRequest = await getClient.GetAsync("/orders");

                    var getResponseData = await getRequest.Content.ReadAsStringAsync();

                    var orders = JsonConvert.DeserializeObject<List<Order>>(getResponseData);
                    
                    
                    Assert.Equal(201, (int)postResponse.StatusCode);
                    Assert.Equal(200, (int)getRequest.StatusCode);
                    Assert.Equal(0,orders[0].Id);
                    Assert.Equal(orderResponse.FirstName,orders[0].FirstName);
                    Assert.Equal(orderResponse.LastName,orders[0].LastName);
                    Assert.Equal(orderResponse.FoodOrder,orders[0].FoodOrder);
                }
            }
        }
        
        [Fact]
        public async void GetAllOrdersTest()
        {
            using (var getAllOrdersClient = new HttpClient {BaseAddress = new Uri("http://localhost:8080")}) //instant from a httpClient(client side)
            {
                var getResponse = await getAllOrdersClient.GetAsync("/orders"); //sending a request from client side & return the response;

                var ordersResponse = JsonConvert.DeserializeObject<List<Order>>(await getResponse.Content.ReadAsStringAsync());
                //???
                
                Assert.Equal(200, (int)getResponse.StatusCode);
                Assert.Empty(ordersResponse);
            }
        }
    }
}