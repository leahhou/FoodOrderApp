using System;
using System.Collections.Generic;
using FoodOrderApp.Models;

namespace FoodOrderApp
{
    public class OrdersDataInMemory : IOrdersData
    {
        public Orders Orders { get; }

        public OrdersDataInMemory()
        {
            Orders = Orders.Instance;
        }
        public Order Create(Order order)
        {
            order.Id = AssignId();
            Orders.AllOrders.Add(order);
            return order;
        }

        public Order Update(Order order)
        {
            var orderIndex = FindById(order.Id);
            Orders.AllOrders[orderIndex] = order;
            return Orders.AllOrders[orderIndex];
        }

        public Order DeleteById(int? orderId)
        {
            if (orderId == null)
                throw new Exception("id cannot be null");
            var orderDeleted = Orders.AllOrders[(int)orderId];
                Orders.AllOrders.RemoveAt((int)orderId);
                return orderDeleted;
        }

        public List<Order> RetrieveAll()
        {
            return Orders.AllOrders;
        }

        public Order RetrieveById(int? orderId)
        {
            return Orders.AllOrders[FindById(orderId)];
        }

        private int AssignId()
        {
            if (Orders.AllOrders.Count == 0)
                return 0;
            return Orders.AllOrders.Count;
        }

        private int FindById(int? orderId)
        {
            if (orderId == null)
                throw new Exception("id cannot be null");
            return Orders.AllOrders.FindIndex(o => o.Id == orderId);
        }
    }
}