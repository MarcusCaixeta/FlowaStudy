namespace FlowaStudy.WebApi.Features.Users.CreateUser
{
    public class CreateUserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; } = 0;
    }
}
