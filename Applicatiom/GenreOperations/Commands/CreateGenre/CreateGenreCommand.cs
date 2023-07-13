using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applicatiom.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel CreateViewModel { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == CreateViewModel.Name);
            if (genre == null)
            {
                genre = _mapper.Map<Genre>(CreateViewModel);
                _dbContext.Genres.Add(genre);
                _dbContext.SaveChanges();

            }
            else
            {
                // Mevcut türün özelliklerini güncelleme
                genre.Name = CreateViewModel.Name;
            }

            _dbContext.SaveChanges();
        }
    }
    public class CreateGenreViewModel
    {
        public string Name { get; set; }
    }
}
