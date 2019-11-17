using System;

namespace FoodOrderApp
{
    public class OrderDoesNotFoundException : Exception
    {
        public  OrderDoesNotFoundException(string message)
            : base(message)
        {
        }
    }
}