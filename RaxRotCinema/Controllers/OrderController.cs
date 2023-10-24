using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Data.Cart;
using RaxRotCinema.Models.ViewModels;
using RaxRotCinema.Repo.IRepository;
using RaxRotCinema.Helper;
using RaxRotCinema.Repo.Servises;

namespace RaxRotCinema.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrderController(IUnitOfWork unitOfWork,
            ShoppingCart shoppingCart,
            IOrderService orderService)
        {
            _unitOfWork = unitOfWork;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var items=_shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems=items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart=_shoppingCart,
                ShoppingCartTotal=_shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public IActionResult IndexAllOrders()
        {
            string userId = "";
            var orders=_orderService.GetOrdersByUserId(userId);
            return View(orders);
        }

        public RedirectToActionResult AddItemToShoppingCart(int id)
        {
            var item=_unitOfWork.Movie.GetMovieById(id);
            if(item != null)
            {
                TempData[TagManager.ToastrSuccess] = "Added";
                _shoppingCart.AddItemToCart(item);
            }

            return RedirectToAction(nameof(Index));
        }

        public RedirectToActionResult RemoveItemFromShoppingCart(int id)
        {
            var item = _unitOfWork.Movie.GetMovieById(id);
            if (item != null)
            {
                TempData[TagManager.ToastrWarning] = "Removed";
                _shoppingCart.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CompleteOrder()
        {
             var items=_shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmailAdress = "";

            _orderService.StoreOrder(items, userId, userEmailAdress);
            _shoppingCart.ClearShoppingCart();

            return View("OrderCompleted");
        }

        public IActionResult OrderCompleted()
        {
            return View();
        }


    }
}
