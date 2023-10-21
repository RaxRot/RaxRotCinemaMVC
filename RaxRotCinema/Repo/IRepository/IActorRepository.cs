using RaxRotCinema.Models;

namespace RaxRotCinema.Repo.IRepository
{
    public interface IActorRepository : IRepository<Actor>
    {
        void Update(Actor actor);
    }
}
