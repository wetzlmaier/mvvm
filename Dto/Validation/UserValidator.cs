using FluentValidation;

namespace Dto.Validation
{
    public class UserValidator : AbstractValidator<IUser>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Lastname is required.");
        }
    }
}
