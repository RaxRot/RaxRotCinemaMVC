namespace RaxRotCinema.Repo.IRepository
{
    public interface IUnitOfWork
    {
        IActorRepository Actor { get; }
       

        void Save();
    }
}
