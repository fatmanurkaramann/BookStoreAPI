using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Applicatiom.GenreOperations.Querys.GetGenres
{
    public class GetGenreQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GenreViewModel> Handle()
        {
            var genres = _dbContext.Genres.Where(e => e.IsActive == true).OrderBy(e=>e.GenreId);
            List<GenreViewModel> genreViewModels=_mapper.Map<List<GenreViewModel>>(genres);

            return genreViewModels;
        }
    }
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
