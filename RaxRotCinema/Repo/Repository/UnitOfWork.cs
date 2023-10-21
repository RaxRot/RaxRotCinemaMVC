using RaxRotCinema.Data;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Repo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

       public IActorRepository Actor { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Actor = new ActorRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
