using FlowaStudy.Domain.Entities;

namespace FlowaStudy.Domain.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
    }
}
