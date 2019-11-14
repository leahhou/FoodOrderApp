using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace FoodOrderApp
{
    public class OrderController
    {
        public readonly IOrdersData OrdersData;
        public OrderController(IOrdersData ordersData)
        {
            OrdersData = ordersData;
        }
        public List<Order> GetAllOrders()
        {
            return OrdersData.RetrieveAll();
        }
        
        public Order GetOrderById(int orderId)
        {
            return OrdersData.RetrieveById(orderId);
        }
        
        public Order CreateOrder(Order order)
        {
            return ValidateOrder(order) ? OrdersData.Create(order) 
                : throw new InvalidOrderException("Error: FirstName or LastName or FoodOrder cannot be null");
        }

        public Order UpdateOrder(Order order)
        {
            return ValidateOrder(order) ? OrdersData.Update(order) 
                : throw new InvalidOrderException("Error: FirstName or LastName or FoodOrder cannot be null");
        }

        public List<Order> DeleteOrderById(int orderId)
        {
            return OrdersData.DeleteById(orderId);
        }

        private bool ValidateOrder(Order order)
        {
            return order.FirstName != null
                   && order.LastName != null
                   && order.FoodOrder != null;
        }
    }
}