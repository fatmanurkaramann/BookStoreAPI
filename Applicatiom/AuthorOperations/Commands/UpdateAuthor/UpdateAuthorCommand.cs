using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applicatiom.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
    public UpdateAuthorCommand(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Author.SingleOrDefault(x=>x.AuthorId==AuthorId);
            _mapper.Map(Model, author);

            _dbContext.SaveChanges();
        }

       
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
    }
}
