using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
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

        public void Send<T>(string exchangeName, T message)
        {
            EnsureConnectionCreated();

            using(var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
                channel.BasicPublish(exchangeName, "web.message", null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
            }
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
                    _connection = _connection ?? _connectionFactory.CreateConnection();
                }
            }
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
}
