using Microsoft.AspNetCore.Mvc;
using producer_queue.Services;
using producer_queue.RabbitMQ;

namespace producer_queue.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase {

        private readonly ILogger<QueueController> _logger;

        public QueueController(ILogger<QueueController> logger) {
            _logger = logger;
        }

        [HttpPost(Name = "NewTicket")]
        public int NewTicket([FromBody]QueueObject queueObject) {
            _logger.LogDebug("Prioridade: {priority}", queueObject.Priority);
            int ticketNumber = QueueService.GetNextTicket(queueObject);
            _logger.LogDebug("Ticket: {ticket}", ticketNumber);
            QueueProducer.Public(ticketNumber);
            return ticketNumber;
        }
    }
}