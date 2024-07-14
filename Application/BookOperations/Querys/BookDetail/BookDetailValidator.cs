using FluentValidation;

namespace BookStore.BookOperations.BookDetail
{
    public class BookDetailValidator:AbstractValidator<BookDetailQuery>
    {
        public BookDetailValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
