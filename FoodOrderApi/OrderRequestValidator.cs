using System;
using System.Net;
using FoodOrderApp.Models;

namespace FoodOrderApi
{
    public static class OrderRequestValidator
    {
        public static bool IsOrderIdValid(Order order, HttpListenerRequest req)
        {
            var routeId = int.Parse(req.Url.Segments[2] ?? throw new Exception());
            if (routeId == order.Id)
                return true;
            throw new InvalidOrderRequestException("Request Denied: Inconsistent Id on the request.");
        }
        
        public static void IsOrderIdExistOnRequestBody(Order order)
        {
            if (order.Id == null)
                throw new InvalidOrderRequestException("Request Denied: No Id is in the Request Body.");
        }
    }
}