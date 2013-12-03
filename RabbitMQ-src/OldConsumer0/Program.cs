using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Consumer.Shared;

using MassTransit;
using MassTransit.Transports.RabbitMq;

namespace OldConsumer0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = Assembly.GetEntryAssembly().GetName().Name;
            Console.WriteLine("I'm an old version of the consumer");
            ServiceBusFactory.New(cfg =>
            {
                cfg.UseRabbitMq();
                cfg.SetDefaultRetryLimit(1);
                // NOTE: Notice that this is a different queue than the other consumer of the same message
                cfg.ReceiveFrom("rabbitmq://localhost/dcc.multi.consumer0");
                cfg.Subscribe(s => s.Consumer<OldArticleUpdateMessageConsumer>());
            });
            Console.WriteLine("waiting for messages");

        }
    }
}
