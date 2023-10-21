using RaxRotCinema.Data;
using RaxRotCinema.Models;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Repo.Repository
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        private readonly ApplicationDbContext _db;

        public ActorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Actor actor)
        {
            _db.Actors.Update(actor);
        }
    }
}
