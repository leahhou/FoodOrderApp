using System;
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
            if(orderId >= OrdersData.Orders.AllOrders.Count|| orderId < 0)
                throw new ArgumentOutOfRangeException(nameof(orderId), "Error: Id does not exist.");
            return OrdersData.RetrieveById(orderId);
        }

        public Order CreateOrder(Order order)
        {
            return ValidateOrder(order)
                ? OrdersData.Create(order)
                : throw new InvalidOrderException("Invalid Order: FirstName, LastName and FoodOrder fields are all required to create an order.");
        }

        public Order UpdateOrder(Order order)
        {
            if(order.Id >= OrdersData.Orders.AllOrders.Count|| order.Id < 0)
                throw new ArgumentOutOfRangeException(nameof(order), "Error: Id does not exist.");
            return ValidateOrder(order)
                ? OrdersData.Update(order)
                : throw new InvalidOrderException("Invalid Order: FirstName, LastName and FoodOrder fields are all required to update an order.");
        }

        public Order DeleteOrderById(int? orderId)
        {
            if(orderId >= OrdersData.Orders.AllOrders.Count|| orderId < 0)
                throw new ArgumentOutOfRangeException(nameof(orderId), "Error: Id does not exist.");
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