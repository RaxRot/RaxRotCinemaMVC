using RaxRotCinema.Data;
using RaxRotCinema.Models;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Repo.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Movie movie)
        {
            _db.Movies.Update(movie);
        }
    }
}
