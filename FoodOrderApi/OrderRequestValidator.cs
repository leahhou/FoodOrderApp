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
            var routeId = int.Parse(req.Url.Segments.LastOrDefault() ?? throw new Exception());
            var requestId = int.Parse(req.Headers["id"]);
            if (routeId == requestId)
                return true;
            throw new PermissionException("Error: Permission Denied.");
        }
    }
}