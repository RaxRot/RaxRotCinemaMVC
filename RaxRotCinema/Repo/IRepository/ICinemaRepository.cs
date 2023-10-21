using RaxRotCinema.Models;

namespace RaxRotCinema.Repo.IRepository
{
    public interface ICinemaRepository : IRepository<Cinema>
    {
        void Update(Cinema cinema);
    }
}
