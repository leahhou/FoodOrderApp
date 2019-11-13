using System.Collections.Generic;

namespace FoodOrderApp
{
    public interface IOrdersData
    {
        Order Create(Order order);
        Order Update(Order order);
        List<Order> DeleteById(int orderId);
        List<Order> RetrieveAll();
        Order RetrieveById(int orderId);
    }
}