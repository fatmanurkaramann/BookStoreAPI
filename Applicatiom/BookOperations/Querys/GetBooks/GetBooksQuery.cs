    using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handler()
        {
            var bookList=_dbContext.Books.Include(x => x.Genre).ToList();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
            //foreach (var item in bookList)
            //{
            //    vm.Add(new BooksViewModel()
            //    {
            //        Title = item.Title,
            //        Genre = ((GenreEnum)item.GenreId).ToString(),
            //        PageCount = item.PageCount,
            //        PublishDate = item.PublishDate.ToString("dd/MM/yyyy"),
            //    });
            //}
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
