using BookStore.Common;
using BookStore.DbOperations;

namespace BookStore.BookOperations.BookDetail
{
    public class BookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public BookDetailQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailViewModel Handle(int Id)
        {
            var book = _dbContext.Books.Where(x => x.Id == Id).SingleOrDefault();
            BookDetailViewModel bookDetail = new BookDetailViewModel();
            bookDetail.Title=book.Title;
            bookDetail.PublishDate = book.PublishDate.ToString();
            bookDetail.Genre = ((GenreEnum)book.GenreId).ToString();
            bookDetail.PageCount = book.PageCount;
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
