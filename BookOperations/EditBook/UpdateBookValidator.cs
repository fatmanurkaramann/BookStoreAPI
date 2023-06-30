using FluentValidation;

namespace BookStore.BookOperations.EditBook
{
    public class UpdateBookValidator:AbstractValidator<EditBookCommand>
    {
        public UpdateBookValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);

        }
    }
}
