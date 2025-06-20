
using FlowaStudy.Domain.Common;

namespace FlowaStudy.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, decimal balance)
        {
            FullName = fullName;
            Email = email;
            Balance = balance;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; } = 0;

    }
}
