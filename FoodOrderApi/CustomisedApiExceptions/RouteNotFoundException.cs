using System;

namespace FoodOrderApi.CustomisedApiExceptions
{
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException(string message)
            : base(message)
        {
        }
    }
}