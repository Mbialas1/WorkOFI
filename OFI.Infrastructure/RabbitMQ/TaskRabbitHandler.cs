using Core.RabbitMQ;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OFI.Infrastructure.Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OFI.Infrastructure.RabbitMQ
{
    public class TaskRabbitHandler : IRabbitMQHandler
    {
        private readonly IModel channel;
        public TaskRabbitHandler(IModel _channel)
        {
            channel = _channel;
        }
        public void SendMessage(object message, ServicesNameEnum servicesNameEnum, string messageName)
        {
            switch (servicesNameEnum)
            {
                case ServicesNameEnum.UsersService:
                    SendMessageToUsersAPI(message, messageName);
                    break;
                default: throw new NotImplementedException();
            }
        }

        public void SendMessageToUsersAPI(object messsage, string messageName)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messsage));

            if (body is null)
                return;

            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object>
            {
                { "MessageType", nameof(messageName) }
             };

            channel.BasicPublish(exchange: "",
                                  routingKey: RabbitHelper.Rabbit_Users_api_Queue,
                                  basicProperties: properties,
                                  body: body);
        }
    }
}
