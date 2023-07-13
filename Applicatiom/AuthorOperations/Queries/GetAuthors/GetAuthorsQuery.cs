using AutoMapper;
using BookStore.Applicatiom.GenreOperations.Querys.GetGenres;
using BookStore.DbOperations;

namespace BookStore.Applicatiom.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<GetAuthorModel> Handle()
        {
            var genres = _dbContext.Authors.OrderBy(e => e.AuthorId).ToList();
            List<GetAuthorModel> authorModel = _mapper.Map<List<GetAuthorModel>>(genres);

            return authorModel;
        }
    }
    public class GetAuthorModel
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

    }
}
