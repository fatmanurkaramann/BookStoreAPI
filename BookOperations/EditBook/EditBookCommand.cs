using BookStore.DbOperations;

namespace BookStore.BookOperations.EditBook
{
    public class EditBookCommand
    {
        public EditBookModel Model { get; set; }
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public EditBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
            {
                throw new InvalidOperationException();
            }
            book.Title=Model.Title;
            book.PublishDate=Model.PublishDate;
            book.GenreId=Model.GenreId;
            book.PageCount=Model.PageCount;

            _dbContext.SaveChanges();
        }

        public class EditBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
