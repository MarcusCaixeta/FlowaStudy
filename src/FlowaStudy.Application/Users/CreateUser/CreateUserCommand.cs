
using MediatR;

namespace FlowaStudy.Application.Users.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserResult>
    {       
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; } = 0;

        public CreateUserCommand(string fullName, string email, decimal balance)
        {
            FullName = fullName;
            Email = email;
            Balance = balance;
        }
    }
}
