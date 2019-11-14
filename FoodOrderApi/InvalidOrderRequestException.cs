using System;

namespace FoodOrderApi
{
    public class InvalidOrderRequestException : Exception
    {
        public InvalidOrderRequestException(string message)
            : base(message)
        {
        }
    }
}