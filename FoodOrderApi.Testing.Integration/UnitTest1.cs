using System;
using System.Collections.Generic;
using System.Net.Http;
using FoodOrderApp;
using Newtonsoft.Json;
using Xunit;

namespace FoodOrderApi.Testing.Integration
{
    public class UnitTest1
    {
        [Fact]
        public async void WhenNewOrderIsMade_PostToNewOrder_OrderAddedSuccessfully()
        {
            using (var postClient = new HttpClient{BaseAddress = new Uri("http://localhost:8080/")})
            {
                var orderToPost = JsonConvert.SerializeObject(
                    new Order("Helena","Franczak","Burritos"));

                var postResponse = await postClient.PostAsync("/orders", new StringContent(orderToPost));

                var postResponseData = await postResponse.Content.ReadAsStringAsync();

                var newOrder = JsonConvert.DeserializeObject<Order>(postResponseData);

                using (var getClient = new HttpClient {BaseAddress = new Uri("http://localhost:8080/")})
                {
                    var request = await getClient.GetAsync("/orders");

                    var getResponseData = await request.Content.ReadAsStringAsync();

                    var orders = JsonConvert.DeserializeObject<List<Order>>(getResponseData);
                    
                    Assert.Equal(1,orders[0].Id);
                    Assert.Equal(newOrder.FirstName,orders[0].FirstName);
                    Assert.Equal(newOrder.LastName,orders[0].LastName);
                    Assert.Equal(newOrder.FoodOrder,orders[0].FoodOrder);
                }
            }
        }
    }
}