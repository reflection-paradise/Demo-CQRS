using Application.RespondModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IMessageBus
{
    public interface IRabbitMQPublisher
    {
        void Publish(string exchange, string routingKey, byte[] body);
    }
}
