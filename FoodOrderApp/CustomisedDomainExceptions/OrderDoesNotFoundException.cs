using System;

namespace FoodOrderApp.CustomisedDomainExceptions
{
    public class OrderDoesNotFoundException : Exception
    {
        public  OrderDoesNotFoundException(string message)
            : base(message)
        {
        }
    }
}