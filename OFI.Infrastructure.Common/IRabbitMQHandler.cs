using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFI.Infrastructure.Common
{
    public interface IRabbitMQHandler
    {
        void SendMessage(object message, ServicesNameEnum servicesNameEnum, string messageName );
    }
}
