using AutoMapper;
using BookStore.Applicatiom.GenreOperations.Commands.CreateGenre;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applicatiom.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel CreateAuthorModel { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var authors = _dbContext.Authors.SingleOrDefault(x => x.Name == CreateAuthorModel.Name);
            if(authors == null)
            {
                authors = _mapper.Map<Author>(CreateAuthorModel);
                _dbContext.Authors.AddRange(authors);
                _dbContext.SaveChanges();
            }
            else
            {
                // Mevcut türün özelliklerini güncelleme
                authors.Name = CreateAuthorModel.Name;
                authors.Lastname = CreateAuthorModel.Lastname;
                authors.Birthday=CreateAuthorModel.Birthday;
            }
            _dbContext.SaveChanges();

        }
    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }

    }
}
