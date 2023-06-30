using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;

namespace BookStore.BookOperations.BookDetail
{
    public class BookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle(int Id)
        {
            var book = _dbContext.Books.Where(x => x.Id == Id).SingleOrDefault();
            BookDetailViewModel bookDetail = _mapper.Map<BookDetailViewModel>(book);
            //bookDetail.Title=book.Title;
            //bookDetail.PublishDate = book.PublishDate.ToString();
            //bookDetail.Genre = ((GenreEnum)book.GenreId).ToString();
            //bookDetail.PageCount = book.PageCount;
            return bookDetail;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
