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
        public const string RabbitConnectionRetryNumberKey = "RabbitMQ:Connection:Retry:Number";
        public const string RabbitConnectionRetryIntervalKey = "RabbitMQ:Connection:Retry:Interval";
        public const string WebDbConnectionStringKey = "DB:ConnectionString";
        public const string WebApiBaseUrlKey = "WebApi:BaseUrl";
        public const string WebDbConnectionRetryNumberKey = "DB:ConnectionRetry:Number";
        public const string WebDbConnectionRetryIntervalKey = "DB:ConnectionRetry:Interval";
    }
}
