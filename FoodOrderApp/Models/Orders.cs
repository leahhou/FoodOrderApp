using System.Collections.Generic;

namespace FoodOrderApp.Models
{
    public class Orders
    {
        public readonly List<Order> AllOrders;

        public static Orders Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Orders();
                }

                return _instance;
            }
        }

        private static Orders _instance;

        private Orders()
        {
            AllOrders = new List<Order>();
        }
    }
}