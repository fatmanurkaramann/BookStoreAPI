using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Applicatiom.AuthorOperations.Queries.AuthorDetail
{
    public class AuthorDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public AuthorDetailQuery(IMapper mapper, IBookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public AuthorDetailModel Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.AuthorId == AuthorId);
            return _mapper.Map<AuthorDetailModel>(author);
        }
    }

    public class AuthorDetailModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
