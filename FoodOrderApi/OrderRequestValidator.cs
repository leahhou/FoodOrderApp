using System;
using System.Linq;
using System.Net;
using FoodOrderApp;
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
            throw new PermissionException("Error: Permission Denied.");
        }
        
        public static void IsOrderIdExistOnRequestBody(Order order)
        {
            if (order.Id == null)
                throw new ApiException("Error: No id on the body.");
        }
    }
}