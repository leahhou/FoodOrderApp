using System;
using System.Net;
using FoodOrderApi.CustomisedApiExceptions;
using FoodOrderApp.Models;

namespace FoodOrderApi
{
    public static class OrderRequestValidator
    {
        public static bool IsOrderIdValid(int orderId, HttpListenerRequest req)
        {
            var routeId = int.Parse(req.Url.Segments[2] ?? throw new InvalidOrderRequestException("Request Denied: No order Id on the Route."));
            if (routeId == orderId)
                return true;
            throw new InvalidOrderRequestException("Request Denied: Inconsistent Id on the request.");
        }
        
        public static void IsOrderIdExistOnRequestBody(Order order)
        {
            if (order.Id == null)
                throw new InvalidOrderRequestException("Request Denied: No order Id is in the Request Body.");
        }
    }
}