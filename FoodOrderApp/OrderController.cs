using System.Collections.Generic;
using FoodOrderApp.Models;

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
            return ValidateOrder(order)
                ? OrdersData.Create(order)
                : throw new InvalidOrderException("Invalid Order: FirstName or LastName or FoodOrder cannot be empty");
        }

        public Order UpdateOrder(Order order)
        {
            return ValidateOrder(order)
                ? OrdersData.Update(order)
                : throw new InvalidOrderException("Invalid Order: FirstName or LastName or FoodOrder cannot be empty");
        }

        public List<Order> DeleteOrderById(int? orderId)
        {
            return OrdersData.DeleteById(orderId);
        }

        private static bool ValidateOrder(Order order)
        {
            return !string.IsNullOrWhiteSpace(order.FirstName)
                   && !string.IsNullOrWhiteSpace(order.LastName)
                   && !string.IsNullOrWhiteSpace(order.FoodOrder);
        }
    }
}