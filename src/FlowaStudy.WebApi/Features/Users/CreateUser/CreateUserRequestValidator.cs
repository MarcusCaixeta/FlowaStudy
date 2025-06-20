using FluentValidation;

namespace FlowaStudy.WebApi.Features.Users.CreateUser
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(user => user.FullName).NotEmpty();
        }
    }
}
