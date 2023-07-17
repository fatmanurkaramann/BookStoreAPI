using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            BookStoreDbContext bookStoreDbContext = serviceProvider.GetRequiredService<BookStoreDbContext>();
            IBookStoreDbContext db = serviceProvider.GetRequiredService<IBookStoreDbContext>();
            using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {

                if (context.Books.Any())
                {
                    return;
                }
                db.Genres.AddRange(
                   new Genre {
                       Name="Personal",
                   },
                    new Genre
                    {
                        Name = "Roman",
                    }
                   );

                var books = new List<Book>
                {
                new Book
                {
                    GenreId =1,
                    Title = "Kanjilal",
                    PageCount = 1,
                    PublishDate = DateTime.Now,

                },
                };
                db.Books.AddRange(books);
                db.SaveChanges();
                bookStoreDbContext.SaveChanges();
            }
        }
   
    }
}
