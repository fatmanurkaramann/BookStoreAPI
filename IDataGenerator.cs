using BookStore.Entities;

namespace BookStore
{
    public interface IDataGenerator
    {
        public List<Book> GetAuthors();
    }
}
