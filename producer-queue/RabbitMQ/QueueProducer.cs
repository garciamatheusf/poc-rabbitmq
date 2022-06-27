using RabbitMQ.Client;
using System.Text;

namespace producer_queue.RabbitMQ {
    public class QueueProducer {
        public static void Public(int ticketNumber) {
            var factory = QueueProducer.CreateFactoryWithDefaultConfiguration();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            var body = Encoding.UTF8.GetBytes((ticketNumber+"").ToArray());
            channel.BasicPublish(exchange: "TICKET_BROKER",
                routingKey: "",
                mandatory: true,
                basicProperties: null,
                body: body);
        } 

        public static ConnectionFactory CreateFactoryWithDefaultConfiguration() {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return new ConnectionFactory() {
                HostName = config.GetSection("RabbitMQ")["Host"],
                Port = int.Parse(config.GetSection("RabbitMQ")["Port"]),
                UserName = config.GetSection("RabbitMQ")["UserName"],
                Password = config.GetSection("RabbitMQ")["Password"]
            };
        }
    }
}
