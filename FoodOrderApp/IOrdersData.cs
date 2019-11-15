using System.Collections.Generic;
using FoodOrderApp.Models;

namespace FoodOrderApp
{
    public interface IOrdersData
    {
         Orders Orders { get; }
        Order Create(Order order);
        Order Update(Order order);
        Order DeleteById(int? orderId);
        List<Order> RetrieveAll();
        Order RetrieveById(int? orderId);
    }
}