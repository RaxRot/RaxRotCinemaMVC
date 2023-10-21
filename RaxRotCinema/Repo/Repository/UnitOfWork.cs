using RaxRotCinema.Data;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Repo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IActorRepository Actor { get; private set; }
        public IProducerRepository Producer { get; private set; }
        public ICinemaRepository Cinema { get; private set; }
        public IMovieRepository Movie { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Actor = new ActorRepository(_db);
            Producer = new ProducerRepository(_db);
            Cinema = new CinemaRepository(_db);
            Movie = new MovieRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
