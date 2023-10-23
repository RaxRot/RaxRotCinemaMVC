namespace RaxRotCinema.Models.ViewModels
{
    public class MovieDropDownVM
    {
        public MovieDropDownVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }


    }
}
