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
            using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {

                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                   new Genre {
                       Name="Personal",
                   },
                    new Genre
                    {
                        Name = "Roman",
                    }
                   );

                var authors = new List<Book>
                {
                new Book
                {
                    GenreId =1,
                    Title = "Kanjilal",
                    PageCount = 1,
                    PublishDate = DateTime.Now,

                },
                };
                context.Books.AddRange(authors);
                context.SaveChanges();
            }
        }
   
    }
}
