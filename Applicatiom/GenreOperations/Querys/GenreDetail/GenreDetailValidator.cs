using FluentValidation;

namespace BookStore.Applicatiom.GenreOperations.Querys.GenreDetail
{
    public class GenreDetailValidator:AbstractValidator<GenreDetailQuery>
    {
        public GenreDetailValidator()
        {
            RuleFor(query=>query.GenreId).GreaterThan(0);
        }
    }
}
