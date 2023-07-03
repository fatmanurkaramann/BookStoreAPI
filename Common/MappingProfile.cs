using AutoMapper;
using BookStore.Applicatiom.GenreOperations.Commands.CreateGenre;
using BookStore.Applicatiom.GenreOperations.Querys.GetGenres;
using BookStore.BookOperations.GetBooks;
using BookStore.Entities;
using static BookStore.BookOperations.BookDetail.BookDetailQuery;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.EditBook.EditBookCommand;

namespace BookStore.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //createbookmodelden book a dönüşüm yapsın
            CreateMap<CreateBookModel, Book>();
            CreateMap<EditBookModel, Book>();

            CreateMap<Book, BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt
            .MapFrom(src=>((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt
            .MapFrom(src => ((GenreEnum)src.GenreId).ToString()));


            CreateMap<GenreViewModel, Genre>();
            CreateMap<CreateGenreViewModel, Genre>();
        }
    }
}
