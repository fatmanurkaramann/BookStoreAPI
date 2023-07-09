using AutoMapper;
using BookStore.Applicatiom.AuthorOperations.Commands.CreateAuthor;
using BookStore.Applicatiom.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Applicatiom.AuthorOperations.Queries.AuthorDetail;
using BookStore.Applicatiom.AuthorOperations.Queries.GetAuthors;
using BookStore.Applicatiom.GenreOperations.Commands.CreateGenre;
using BookStore.Applicatiom.GenreOperations.Querys.GenreDetail;
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
            .MapFrom(src=>src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt
            .MapFrom(src => src.Genre.Name));


            //Genre
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreViewModel, Genre>();
            
            //Author
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, GetAuthorModel>();
            CreateMap<UpdateAuthorModel,Author>();
            CreateMap<Author, AuthorDetailModel>();
        }
    }
}
