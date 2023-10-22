using System.ComponentModel.DataAnnotations;

namespace RaxRotCinema.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Logo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }

        //relationship
        public List<Movie>? Movies { get; set; }
    }
}
