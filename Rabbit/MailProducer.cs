using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rabbit
{
    public class MailProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public MailProducer(string hostName, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = queueName;
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMail(Mail mail)
        {
            var message = JsonSerializer.Serialize(mail);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            Console.WriteLine("Sent message: {0}", message);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }

}
