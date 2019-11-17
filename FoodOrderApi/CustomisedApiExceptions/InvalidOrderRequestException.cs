using System;

namespace FoodOrderApi.CustomisedApiExceptions
{
    public class InvalidOrderRequestException : Exception
    {
        public InvalidOrderRequestException(string message)
            : base(message)
        {
        }
    }
}