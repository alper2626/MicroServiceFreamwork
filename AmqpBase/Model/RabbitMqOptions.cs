﻿
namespace AmqpBase.Model
{
    public class RabbitMqOptions
    {
        public string Host { get; set; }
        
        public ushort Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
