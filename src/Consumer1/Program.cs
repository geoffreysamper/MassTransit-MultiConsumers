using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Consumer.Shared;

using MassTransit;

namespace Consumer1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusFactory.New(cfg =>
            {
                cfg.UseRabbitMq();

                // NOTE: Notice that this is a different queue than the other consumer of the same message
                cfg.ReceiveFrom("rabbitmq://localhost/dcc.multi.consumer1");
                cfg.Subscribe(s => s.Consumer<ArticleUpdateMessageConsumer>());
                
            });

            Console.WriteLine("Waiting on messages");
        }
    }
}
