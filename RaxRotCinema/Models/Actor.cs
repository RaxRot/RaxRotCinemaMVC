using System.ComponentModel.DataAnnotations;

namespace RaxRotCinema.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProfilePictureURL { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Bio { get; set; }

        //relationship

        public List<Actor_Movie> Actor_Movies { get; set; }
       
    }
}
