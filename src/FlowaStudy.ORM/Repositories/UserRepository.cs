using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using FlowaStudy.ORM.Contexts;

namespace FlowaStudy.ORM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EfContext _context;
        public UserRepository(EfContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.User.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
