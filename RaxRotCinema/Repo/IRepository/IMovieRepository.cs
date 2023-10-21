using RaxRotCinema.Models;

namespace RaxRotCinema.Repo.IRepository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie movie);
    }
}
