using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Consumer.Shared;

using MassTransit;
using MassTransit.Log4NetIntegration;

using Messages;

namespace Consumer2
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            Console.Title = Assembly.GetEntryAssembly().GetName().Name;
           
            ServiceBusFactory.New(cfg =>
            {
                cfg.UseMsmq();
                cfg.UseMulticastSubscriptionClient();   
                cfg.UseLog4Net();
                cfg.UseJsonSerializer();
                cfg.UseControlBus();
                cfg.SetDefaultRetryLimit(1);
                // NOTE: Notice that this is a different queue than the other consumer of the same message
                cfg.ReceiveFrom("msmq://localhost/dcc.multi.consumer2");
                
                
             cfg.Subscribe(s => s.Consumer<ArticleUpdateMessageConsumer>());
            });
            
            Console.WriteLine("Waiting on messages");
            Console.ReadLine();
        }
    }
}
