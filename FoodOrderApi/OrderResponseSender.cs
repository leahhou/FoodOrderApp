using System.Collections.Generic;
using System.Net;
using System.Text;
using FoodOrderApp;
using Newtonsoft.Json;

namespace FoodOrderApi
{
    public class OrderResponseSender
    {
        public void SendSuccessResponse(HttpListenerResponse res, int statusCode, List<Order> orders)
        {
            var responseBody = JsonConvert.SerializeObject(orders);
            PrepareSuccessBuffer(res,responseBody,statusCode);
        }
        
        public void SendSuccessResponse(HttpListenerResponse res, int statusCode, Order order)
        {
            var responseBody = JsonConvert.SerializeObject(order);
            PrepareSuccessBuffer(res,responseBody,statusCode);
        }

        public void SendFailResponse(HttpListenerResponse res, int statusCode)
        {
            res.StatusCode = statusCode;
            res.Close();
        }

        private async void PrepareSuccessBuffer(HttpListenerResponse res, string responseBody, int statusCode)
        {
            var buffer = Encoding.UTF8.GetBytes(responseBody);
            res.ContentLength64 = buffer.Length;
            res.StatusCode = statusCode;
            
            await res.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            res.Close();
        }
        
    }
}