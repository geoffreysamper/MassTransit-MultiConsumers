﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Consumer.Shared;

using MassTransit;

namespace OldConsumer0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I'm an old version of the consumer");

            ServiceBusFactory.New(cfg =>
            {
                cfg.UseRabbitMq();

                // NOTE: Notice that this is a different queue than the other consumer of the same message
                cfg.ReceiveFrom("rabbitmq://localhost/dcc.multi.consumer0");
                cfg.Subscribe(s => s.Consumer<OldArticleUpdateMessageConsumer>());
            });
            Console.WriteLine("waiting for messages");

        }
    }
}