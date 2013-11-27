using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MassTransit;
using MassTransit.Log4NetIntegration;

using Messages;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();


            Console.Title = Assembly.GetEntryAssembly().GetName().Name;
         
            
            
            
            Bus.Initialize(sbc =>
            {
                sbc.UseLog4Net();
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("rabbitmq://localhost/dcc.multi.publisher");
             sbc.Subscribe(subs => subs.Handler<ArticleUpdateMessage>(msg => Console.WriteLine(msg.ArticleId)));
            });

            Console.WriteLine("Overiew:");

            Console.WriteLine("1) send and throw an error on consumer1");
            Console.WriteLine("2) send and throw an error on consumer2");
            Console.WriteLine("3) send and throw an error on consomer 1 and 2");
            Console.WriteLine("anykey) send a normal message");

            Console.WriteLine("press a key to start");
            while (true)
            {
                ConsoleKeyInfo choice = Console.ReadKey();
                string throwErrorOn = string.Empty;
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        throwErrorOn = "consumer1";
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        throwErrorOn = "consumer2";
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        throwErrorOn = "all";
                        break;
                    default:
                        break;

                }


                Bus.Instance.Publish(new ArticleUpdateMessage { ThrowError = throwErrorOn, ArticleId = "dmf1031545645", CreationDate = DateTime.Now });

            }



        }
    }
}
