using BookStore.DbOperations;

namespace BookStore.Applicatiom.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public DeleteGenreCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Genres.SingleOrDefault(x => x.GenreId == GenreId);
            _dbContext.Genres.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
