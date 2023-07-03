using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
namespace BookStore.DbOperations
{
    public class BookStoreDbContext:DbContext
    {
        //   protected override void OnConfiguring
        //(DbContextOptionsBuilder optionsBuilder)
        //   {
        //       optionsBuilder.UseInMemoryDatabase(databaseName: "BookStoreDb");
        //   }
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; } 
        public DbSet<Genre> Genres { get; set; }

    }
}
