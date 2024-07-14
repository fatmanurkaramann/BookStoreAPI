using FluentValidation;

namespace BookStore.Applicatiom.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreValidator()
        {
           RuleFor(x=>x.model.Name).MinimumLength(2).When(x=>x.model.Name != String.Empty);
        }
    }
}
