using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    public class WebQueueChannel: IDisposable
    {
        private readonly IModel _channel;

        public WebQueueChannel(IModel channel)
        {
            _channel = channel;
        }

        public void Dispose()
        {
            _channel?.Close();
        }
    }
}
