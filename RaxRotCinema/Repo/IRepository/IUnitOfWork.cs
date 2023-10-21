namespace RaxRotCinema.Repo.IRepository
{
    public interface IUnitOfWork
    {
        IActorRepository Actor { get; }
        ICinemaRepository Cinema { get; }
        IMovieRepository Movie { get; }
        IProducerRepository Producer { get; }
       

        void Save();
    }
}
