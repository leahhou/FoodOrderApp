using System;

namespace FoodOrderApi
{
    public class PermissionException : Exception
    {
        public PermissionException(string message)
            : base(message)
        {
        }
    }
}