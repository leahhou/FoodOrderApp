using System;

namespace FoodOrderApp
{
    public class InvalidOrderException : Exception
    {
        public InvalidOrderException(string message)
            : base(message)
        {
        }
    }
}