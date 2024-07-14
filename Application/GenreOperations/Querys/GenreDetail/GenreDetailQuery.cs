using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applicatiom.GenreOperations.Querys.GenreDetail
{
    public class GenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext; 
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genre=_dbContext.Genres.Where(x=>x.IsActive && x.GenreId == GenreId).SingleOrDefault();
            if (genre == null)
                throw new InvalidOperationException("Kitap türü bulunamadı");
            GenreDetailViewModel genreDetail = _mapper.Map<GenreDetailViewModel>(genre);
            return genreDetail;
        }
    }
    public class GenreDetailViewModel
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
