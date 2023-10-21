using RaxRotCinema.Data;
using RaxRotCinema.Models;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Repo.Repository
{
    public class ProducerRepository : Repository<Producer>, IProducerRepository
    {
        private readonly ApplicationDbContext _db;

        public ProducerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Producer producer)
        {
            _db.Producers.Update(producer);
        }
    }
}
