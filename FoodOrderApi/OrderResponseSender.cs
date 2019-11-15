using System.Collections.Generic;
using System.Net;
using System.Text;
using FoodOrderApp;
using FoodOrderApp.Models;
using Newtonsoft.Json;

namespace FoodOrderApi
{
    public class OrderResponseSender
    {
        public string GetResponseBody(List<Order> orders)
        {
            return JsonConvert.SerializeObject(orders);
        }
        
        public string GetResponseBody(Order order)
        {
            return JsonConvert.SerializeObject(order);
        }
        
        public void SendSuccessResponse(HttpListenerResponse res, HttpStatusCode statusCode, string responseBody)
        {
            PrepareBuffer(res,statusCode,responseBody);
        }

        public void SendFailResponse(HttpListenerResponse res, HttpStatusCode statusCode)
        {
            res.StatusCode = (int)statusCode;
            res.Close();
        }
        public void SendFailResponseWithMessage(HttpListenerResponse res, HttpStatusCode statusCode, string responseBody)
        {
            PrepareBuffer(res, statusCode, responseBody);
        }

        private async void PrepareBuffer(HttpListenerResponse res, HttpStatusCode statusCode,string responseBody)
        {
            var buffer = Encoding.UTF8.GetBytes(responseBody);
            res.ContentLength64 = buffer.Length;
            res.StatusCode = (int)statusCode;
            
            await res.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            res.Close();
        }
        
    }
}