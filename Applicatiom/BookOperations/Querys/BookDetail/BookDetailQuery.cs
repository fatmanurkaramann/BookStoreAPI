using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.BookDetail
{
    public class BookDetailQuery
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=>x.Genre).Where(x => x.Id == BookId).SingleOrDefault();
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
