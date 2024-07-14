using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Applicatiom.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.AuthorId == AuthorId);
            var hasAssociatedBooks = _dbContext.Books.Any(b => b.AuthorId == AuthorId);

            if (hasAssociatedBooks)
            {
                // Author has associated books, cannot be deleted
                throw new InvalidOperationException("Cannot delete author. They have associated books.");
            }

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
