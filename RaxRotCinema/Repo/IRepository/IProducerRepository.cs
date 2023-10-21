using RaxRotCinema.Models;

namespace RaxRotCinema.Repo.IRepository
{
    public interface IProducerRepository : IRepository<Producer>
    {
        void Update(Producer producer);
    }
}
