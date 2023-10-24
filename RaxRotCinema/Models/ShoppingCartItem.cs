using System.ComponentModel.DataAnnotations;

namespace RaxRotCinema.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Movie Movie { get; set; }
        public int Amount { get; set; }

        public string ShoppingCarId { get; set; }
    }
}
