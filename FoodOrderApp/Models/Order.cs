using System;

namespace FoodOrderApp
{
    public class Order
    {
        public int Id;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FoodOrder { get; private set; }
        public DateTime TimeOfOrder { get; private set; }

        public Order( string firstName, string lastName, string foodOrder)
        {
            FirstName = firstName;
            LastName = lastName;
            FoodOrder = foodOrder;
            TimeOfOrder = DateTime.Now;
        }
    }
}