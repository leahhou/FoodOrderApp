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
            var id = int.Parse(req.Url.Segments.LastOrDefault() ?? throw new Exception("Invalid route."));
            return id == order.Id;
        }

//        private static bool IsAllFieldsFilled(Order order)
//        {
//            return  order.FirstName != null
//                    || order.LastName != null
//                    || order.FoodOrder != null;
//        }
    }
}