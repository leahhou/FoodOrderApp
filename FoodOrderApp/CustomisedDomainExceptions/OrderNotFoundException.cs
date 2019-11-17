using System;

namespace FoodOrderApp.CustomisedDomainExceptions
{
    public class OrderNotFoundException : Exception
    {
        public  OrderNotFoundException(string message)
            : base(message)
        {
        }
    }
}