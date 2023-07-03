using FluentValidation;

namespace BookStore.Applicatiom.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreValidator()
        {
            RuleFor(query => query.CreateViewModel.GenreName).NotEmpty().MinimumLength(2);
        }
    }
}
