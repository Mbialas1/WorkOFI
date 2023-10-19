using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.Helpers
{
    public class ServicesHelper
    {
        public const string User_api_services_configuration = "UserApiSettings:BaseUrl";
        public const string Redis_task_services_configuration = "Redis:Configuration";
        public const string Rabbit_host_configuration = "RabbitMQ:Host";
        public const string Rabbit_port_configuration = "RabbitMQ:Port";
        public const string Rabbit_user_configuration = "RabbitMQ:Username";
        public const string Rabbit_password_configuration = "RabbitMQ:Password";
    }
}
