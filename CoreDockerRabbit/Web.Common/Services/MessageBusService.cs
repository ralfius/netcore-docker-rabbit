using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common.Services
{
    public class MessageBusService: IMessageBusService
    {
        private IConnection _connection;
        private ConnectionFactory _connectionFactory;
        private IConfiguration _configuration;
        private object _lock = new Object();

        public MessageBusService(IConfiguration configuration)
        {
            _configuration = configuration;
            EnsureConnectionFactoryCreated();
            EnsureConnectionCreated();
        }

        public void Send<T>(string queueName, T message)
        {
            EnsureConnectionCreated();

            using(var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queueName, durable: true, autoDelete: false, exclusive: false);
                channel.BasicPublish(string.Empty, queueName, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
            }
        }

        public WebQueueChannel GetChannel<T>(string queueName, Action<T> action)
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queueName, durable: true, autoDelete: false, exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var receivedObject = JsonConvert.DeserializeObject<T>(message);

                try
                {
                    action(receivedObject);
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    //TODO add logging
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

            return new WebQueueChannel(channel);
        }

        private void EnsureConnectionFactoryCreated()
        {
            if (_connectionFactory == null)
            {
                lock (_lock)
                {
                    if (_connectionFactory == null)
                    {
                        _connectionFactory = new ConnectionFactory();
                        _connectionFactory.HostName = _configuration[Constants.RabbitHostnameKey];
                        _connectionFactory.UserName = _configuration[Constants.RabbitUserNameKey];
                        _connectionFactory.Password = _configuration[Constants.RabbitPasswordKey];
                    }
                }
            }
        }

        private void EnsureConnectionCreated()
        {
            if (_connection == null && _connectionFactory != null)
            {
                lock (_lock)
                {
                    _connection = _connection ??
                        Policy
                            .Handle<BrokerUnreachableException>()
                            .WaitAndRetry(
                                int.Parse(_configuration[Constants.RetryNumberKey]),
                                attempt => TimeSpan.FromSeconds(int.Parse(_configuration[Constants.RetryIntervalKey])))
                            .Execute(() => _connectionFactory.CreateConnection());
                }
            }
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
}
