using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace consumer_queue.RabbitMQ
{
    internal class QueueConsumer {
        public static void Receive() {
            try {
                var factory = CreateFactoryWithDefaultConfiguration();
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) => {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    channel.BasicAck(ea.DeliveryTag, false);
                    Thread.Sleep(3000);
                };
                channel.BasicConsume(queue: "Q1",
                                     autoAck: false,
                                     consumer: consumer);

                Console.ReadLine();
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        private static ConnectionFactory CreateFactoryWithDefaultConfiguration() {
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
