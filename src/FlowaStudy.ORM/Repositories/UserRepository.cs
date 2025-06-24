using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using FlowaStudy.ORM.Contexts;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
