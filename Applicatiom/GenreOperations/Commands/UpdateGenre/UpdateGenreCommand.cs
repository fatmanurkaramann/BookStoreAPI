using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Applicatiom.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int GenreId { get; set; }
        public UpdateGenreViewModel model { get; set; }
        public UpdateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.GenreId == GenreId);
            if (_dbContext.Genres.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.GenreId != GenreId))
                throw new InvalidOperationException("Aynı isimli kitap türü zaten var");

            genre.Name = model.Name.Trim() == default ? genre.Name : model.Name;
            genre.IsActive = model.IsActive;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateGenreViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

}
