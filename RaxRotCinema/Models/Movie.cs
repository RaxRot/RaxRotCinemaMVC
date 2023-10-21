using RaxRotCinema.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaxRotCinema.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
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

        
        public MovieCategory MovieCategory { get; set; }

        //relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
        
        //ciname
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        //producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set;}
    }
}
