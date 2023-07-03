using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applicatiom.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreViewModel CreateViewModel { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == CreateViewModel.GenreName);
            if (genre == null)
                throw new InvalidOperationException("Kitap türü mevcut");
            genre = _mapper.Map<Genre>(CreateViewModel);
            _dbContext.Genres.AddRange(genre);
            _dbContext.SaveChanges();
        }
    }
    public class CreateGenreViewModel
    {
        public string GenreName { get; set; }
    }
}
