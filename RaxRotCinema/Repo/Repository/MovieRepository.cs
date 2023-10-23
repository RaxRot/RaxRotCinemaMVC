using Microsoft.EntityFrameworkCore;
using RaxRotCinema.Data;
using RaxRotCinema.Models;
using RaxRotCinema.Models.ViewModels;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Repo.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public MovieRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Movie movie)
        {
            _db.Movies.Update(movie);
        }

        public Movie GetMovieById(int id)
        {
            var movieDetail=_db.Movies
                .Include(x=>x.Cinema)
                .Include(y=>y.Producer)
                .Include(z=>z.Actors_Movies)
                .ThenInclude(c=>c.Actor)
                .FirstOrDefault(v=>v.Id==id);

            return movieDetail;
        }

        public MovieDropDownVM GetMovieDropDownVMValues()
        {
            var response = new MovieDropDownVM()
            {
                Actors = _db.Actors.OrderBy(x => x.FullName).ToList(),
                Cinemas = _db.Cinemas.OrderBy(x => x.Name).ToList(),
                Producers = _db.Producers.OrderBy(x => x.FullName).ToList()
            };

            return response;
        }

        public void AddNewMovie(MovieVM data)
        {
            
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                ShortDescription = data.ShortDescription,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory=data.MovieCategory,
                ProducerId=data.ProducerId
            };

            _db.Movies.Add(newMovie);
            _db.SaveChanges();

            //AddMovieActors

            foreach(var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId= actorId
                };

                _db.Actor_Movies.Add(newActorMovie);
            }
            _db.SaveChanges();
        }

        public void UpdateNewMovie(MovieVM data)
        {
            var dbMovie = _db.Movies.FirstOrDefault(x => x.Id == data.Id);
            if (dbMovie != null)
            {

                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.ShortDescription = data.ShortDescription;
                dbMovie.Price = data.Price;
                dbMovie.ImageUrl = data.ImageUrl;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;

                _db.SaveChanges();
            }
            //Remove existActor
            var existingActorsDb=_db.Actor_Movies.Where(x=>x.MovieId == data.Id).ToList();
            _db.Actor_Movies.RemoveRange(existingActorsDb);
            _db.SaveChanges();
            //AddMovieActors

            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };

                _db.Actor_Movies.Update(newActorMovie);
            }
            _db.SaveChanges();
        }

        public void DeleteMovie(MovieVM movieVM)
        {
            if (movieVM != null)
            {
                
                Movie movieToDelete = new Movie
                {
                    Id = movieVM.Id
                };

                
                _db.Movies.Remove(movieToDelete);
                _db.SaveChanges();
            }
            
            var existingActorsDb = _db.Actor_Movies.Where(x => x.MovieId == movieVM.Id).ToList();
            _db.Actor_Movies.RemoveRange(existingActorsDb);
            _db.SaveChanges();
        }


    }
}
