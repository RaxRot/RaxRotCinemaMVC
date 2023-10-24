using RaxRotCinema.Models;

namespace RaxRotCinema.Repo.Servises
{
    public interface IOrderService
    {
        void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        List<Order> GetOrdersByUserId(string userId);
    }
}
