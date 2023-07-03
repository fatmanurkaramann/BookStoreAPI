using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applicatiom.GenreOperations.Querys.GenreDetail
{
    public class GenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext; 
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var genre=_dbContext.Genres.Where(x=>x.IsActive && x.GenreId == GenreId).SingleOrDefault();
            if (genre == null)
                throw new InvalidOperationException("Kitap türü bulunamadı");
            GenreDetailViewModel bookDetail = _mapper.Map<GenreDetailViewModel>(genre);
            return bookDetail;
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
