using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MassTransit;
using Messages;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("rabbitmq://localhost/dcc.multi.publisher");
                sbc.Subscribe(subs => subs.Handler<ArticleUpdateMessage>(msg => Console.WriteLine(msg.ArticleId)));
            });

            Console.WriteLine("press a key to publish a message");
            while (true)
            {
                Console.ReadLine();
                Bus.Instance.Publish(new ArticleUpdateMessage { ArticleId = "dmf1031545645", CreationDate = DateTime.Now });
            }

            

        }
    }
}
