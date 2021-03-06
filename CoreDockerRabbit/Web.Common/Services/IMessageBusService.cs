using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common.Services
{
    public interface IMessageBusService: IDisposable
    {
        void Send<T>(string queueName, T message);
        WebQueueChannel GetChannel<T>(string queueName, Action<T> action);
    }
}
