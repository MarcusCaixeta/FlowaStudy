using FlowaStudy.Application.Messaging.Interfaces;

namespace FlowaStudy.Messaging.Service
{
    public class KafkaConsumerHandler : IKafkaConsumerHandler
    {
        private readonly ILogRepository _logRepository;

        public KafkaConsumerHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task HandleAsync(string message)
        {
            try
            {
                await _logRepository.SaveAsync("asset-transaction", message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
