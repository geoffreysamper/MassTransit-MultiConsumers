using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using MassTransit;
using MassTransit.Log4NetIntegration;

namespace Consumer.Shared
{
    public class ConsumerRunner
    {
        private string consumerName;

        private string _consumerUri;

        public ConsumerRunner()
        {
            this.consumerName = Assembly.GetEntryAssembly().GetName().Name;
            this._consumerUri = "rabbitmq://localhost/dcc.multi." + this.consumerName.ToLowerInvariant();
        }

        public void Run()
        {
            log4net.Config.XmlConfigurator.Configure();
            Console.Title = Assembly.GetEntryAssembly().GetName().Name;

            ServiceBusFactory.New(cfg =>
            {
                cfg.SetDefaultRetryLimit(1);
                cfg.UseRabbitMq();
                cfg.UseLog4Net();
                cfg.ReceiveFrom(_consumerUri);
                cfg.Subscribe(s => s.Consumer<ArticleUpdateMessageConsumer>());
            });

            Console.WriteLine("Waiting on messages");
        }


    }
}
