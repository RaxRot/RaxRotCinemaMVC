using System.ComponentModel.DataAnnotations;

namespace RaxRotCinema.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string ProfilePictureURL { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Bio { get; set; }

        //Relationship
        public List<Movie> Movies { get; set; }
    }
}
