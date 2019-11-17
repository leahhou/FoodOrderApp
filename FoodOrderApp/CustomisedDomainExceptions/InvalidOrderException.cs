using System;

namespace FoodOrderApp.CustomisedDomainExceptions
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException(string message)
            : base(message)
        {
        }
    }
}