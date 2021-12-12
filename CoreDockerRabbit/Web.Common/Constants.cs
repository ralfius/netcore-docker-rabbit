using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common
{
    public static class Constants
    {
        public const string ProcessQueueName = "processes";
        public const string RabbitHostnameKey = "RabbitMQ:Connection:Hostname";
        public const string RabbitUserNameKey = "RabbitMQ:Connection:UserName";
        public const string RabbitPasswordKey = "RabbitMQ:Connection:Password";
        public const string RetryNumberKey = "Retry:Number";
        public const string RetryIntervalKey = "Retry:Interval";
        public const string WebDbConnectionStringKey = "DB:ConnectionString";
        public const string WebApiBaseUrlKey = "WebApi:BaseUrl";
    }
}
