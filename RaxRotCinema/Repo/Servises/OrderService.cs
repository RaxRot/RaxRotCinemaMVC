using Microsoft.EntityFrameworkCore;
using RaxRotCinema.Data;
using RaxRotCinema.Models;

namespace RaxRotCinema.Repo.Servises
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            var orders= _context.Orders
                .Include(x=>x.OrderItems)
                .ThenInclude(x=>x.Movie)
                .Where(x=>x.UserId == userId)
                .ToList();

            return orders;
        }

        public void StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email=userEmailAddress
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId=item.Movie.Id,
                    OrderId=order.Id,
                    Price=item.Movie.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();
        }
    }
}
