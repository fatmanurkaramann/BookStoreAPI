using BookStore.BookOperations.CreateBook;
using FluentValidation;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookValidator: AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);

        }
    }
}
