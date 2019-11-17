using System;
using System.Collections.Generic;
using FoodOrderApp.Models;

namespace FoodOrderApp
{
    public class OrderController
    {
        private readonly IOrdersData _ordersData;

        public OrderController(IOrdersData ordersData)
        {
            _ordersData = ordersData;
        }

        public List<Order> GetAllOrders()
        {
            return _ordersData.RetrieveAll();
        }

        public Order GetOrderById(int? orderId)
        {
            ThrowOrderDoesNotFoundException(orderId);
            return _ordersData.RetrieveById(orderId);
        }

        public Order CreateOrder(Order order)
        {
            return ValidateAllFields(order)
                ? _ordersData.Create(order)
                : throw new InvalidOrderException("Invalid Order: FirstName, LastName and FoodOrder fields are all required to create an order.");
        }

        public Order ReplaceOrder(Order order)
        {
            ThrowOrderDoesNotFoundException(order.Id);
            return ValidateAllFields(order)
                ? _ordersData.Update(order)
                : throw new InvalidOrderException("Invalid Order: FirstName, LastName and FoodOrder fields are all required to replace an order.");
        }
        
        public Order UpdateOrder(Order order)
        {
            ThrowOrderDoesNotFoundException(order.Id);
            return ValidateOneField(order)
                ? throw new InvalidOrderException(
                    "Invalid Order: Either FirstName, LastName or FoodOrder fields is required to update an order.")
                : _ordersData.Update(order);
        }

        public Order DeleteOrderById(int? orderId)
        {
            ThrowOrderDoesNotFoundException(orderId);
            return _ordersData.DeleteById(orderId);
        }

        private static bool ValidateAllFields(Order order)
        {
            return !string.IsNullOrWhiteSpace(order.FirstName)
                   && !string.IsNullOrWhiteSpace(order.LastName)
                   && !string.IsNullOrWhiteSpace(order.FoodOrder);
        }
        
        private static bool ValidateOneField(Order order)
        {
            return string.IsNullOrWhiteSpace(order.LastName)
                   && string.IsNullOrWhiteSpace(order.LastName)
                   && string.IsNullOrWhiteSpace(order.FoodOrder);
        }

        private void ThrowOrderDoesNotFoundException(int? orderId)
        {
            if(orderId >= _ordersData.Orders.AllOrders.Count|| orderId < 0)
                throw new OrderDoesNotFoundException( "Error: Id does not exist.");
        }
    }
}