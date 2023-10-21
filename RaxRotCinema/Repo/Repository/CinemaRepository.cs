using RaxRotCinema.Data;
using RaxRotCinema.Models;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Repo.Repository
{
    public class CinemaRepository : Repository<Cinema>, ICinemaRepository
    {
        private readonly ApplicationDbContext _db;

        public CinemaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Cinema cinema)
        {
            _db.Cinemas.Update(cinema);
        }
    }
}
