using FlowaStudy.Application.Messaging.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlowaStudy.WebApi.Features.Kafka
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly IKafkaProducer _producer;

        public KafkaController(IKafkaProducer producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] string msg)
        {
            await _producer.SendAsync("teste-topic", msg);
            return Ok("Mensagem enviada");
        }
    }
}
