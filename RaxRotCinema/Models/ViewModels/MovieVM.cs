using RaxRotCinema.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RaxRotCinema.Models.ViewModels
{
    public class MovieVM
    {

        public int Id { get; set; }


        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }


        [Required]
        public MovieCategory MovieCategory { get; set; }

        //relationships
        [Required]
        [Display(Name ="Select actor(s)")]
        public List<int> ActorIds { get; set; }

        [Required]
        [Display(Name ="Cinema")]
        public int CinemaId { get; set; }
        [Required]
        [Display(Name = "Producer")]
        public int ProducerId { get; set; }
        
    }
}
