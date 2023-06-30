using BookStore.Common;
using BookStore.DbOperations;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<BooksViewModel> Handler()
        {
            var bookList=_dbContext.Books.ToList();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var item in bookList)
            {
                vm.Add(new BooksViewModel()
                {
                    Title = item.Title,
                    Genre = ((GenreEnum)item.GenreId).ToString(),
                    PageCount = item.PageCount,
                    PublishDate = item.PublishDate.ToString("dd/MM/yyyy"),
                });
            }
            return vm;

        }
    }
    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
