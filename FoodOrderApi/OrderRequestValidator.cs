using System;
using System.Linq;
using System.Net;
using FoodOrderApp;

namespace FoodOrderApi
{
    public static class OrderRequestValidator
    {
        public static bool IsOrderIdValid(Order order, HttpListenerRequest req)
        {
            var id = int.Parse(req.Url.Segments.LastOrDefault() ?? throw new Exception());
            if (id == order.Id)
                return true;
            throw new PermissionException("Error: You are not allowed in this route.");
        }
        
    }
}