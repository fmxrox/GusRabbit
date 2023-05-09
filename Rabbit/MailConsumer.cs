using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit
{
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text.Json;

    public class MailConsumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public MailConsumer(string hostName, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = queueName;
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Received message: {0}", message);
                var mail = JsonSerializer.Deserialize<Mail>(message);

                if (!string.IsNullOrWhiteSpace(mail.To))
                {
                    using var client = new SmtpClient();
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("your_email@gmail.com", "your_password");
                    client.EnableSsl = true;

                    using var messageToSend = new MailMessage(mail.From, mail.To, mail.Subject, mail.Body);
                    client.Send(messageToSend);
                    Console.WriteLine("Sent mail to {0}", mail.To);
                }
            };
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);


        }
    }
}
