using RaxRotCinema.Models;
using RaxRotCinema.Models.ViewModels;

namespace RaxRotCinema.Repo.IRepository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie movie);

        Movie GetMovieById(int id);

        MovieDropDownVM GetMovieDropDownVMValues();

        void AddNewMovie(MovieVM data);
        void UpdateNewMovie(MovieVM data);

        void DeleteMovie(MovieVM movieVM);


    }
}
