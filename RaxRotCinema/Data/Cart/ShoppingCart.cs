using Microsoft.EntityFrameworkCore;
using RaxRotCinema.Models;

namespace RaxRotCinema.Data.Cart
{
    public class ShoppingCart
    {
        private ApplicationDbContext _context;


        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationDbContext>();

            string cartId=session.GetString("CartId")?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId,
            };
        }

        public List<ShoppingCartItem>GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(x => x.ShoppingCarId == ShoppingCartId)
                .Include(n => n.Movie).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total=_context.ShoppingCartItems
                .Where(x=>x.ShoppingCarId==ShoppingCartId)
                .Select(n=>n.Movie.Price*n.Amount)
                .Sum();

            return total;
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem=_context.ShoppingCartItems
                .FirstOrDefault(x=>x.Movie.Id==movie.Id && x.ShoppingCarId==ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCarId = ShoppingCartId,
                    Movie = movie,
                    Amount=1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems
               .FirstOrDefault(x => x.Movie.Id == movie.Id && x.ShoppingCarId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
           
            _context.SaveChanges();
        }

        public void ClearShoppingCart()
        {
            var items= _context.ShoppingCartItems
                .Where(x => x.ShoppingCarId == ShoppingCartId)
                .Include(n => n.Movie)
                .ToList();

            _context.ShoppingCartItems.RemoveRange(items);
            _context.SaveChanges();

            ShoppingCartItems = new List<ShoppingCartItem>();
        }

    }
}
