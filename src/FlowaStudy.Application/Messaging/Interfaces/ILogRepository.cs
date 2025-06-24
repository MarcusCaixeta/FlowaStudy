
namespace FlowaStudy.Application.Messaging.Interfaces
{
    public interface ILogRepository
    {
        Task SaveAsync(string key, string json, CancellationToken cancellationToken = default);
    }
}
