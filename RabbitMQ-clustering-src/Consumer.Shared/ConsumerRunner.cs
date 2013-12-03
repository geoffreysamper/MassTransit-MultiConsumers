using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

using MassTransit;
using MassTransit.Log4NetIntegration;

namespace Consumer.Shared
{
    public class ConsumerRunner
    {
        private string _consumerUri;
        
        public ConsumerRunner()
        {

            this._consumerUri = this.GetRabittMqUrl();
        }

        public string GetRabittMqUrl()
        {
            bool isCluster = false;

            bool.TryParse(ConfigurationManager.AppSettings["rabbitserverIsCluster"], out isCluster);

            string server = ConfigurationManager.AppSettings["rabbitserver"];

            if (string.IsNullOrEmpty(server))
            {
                server = "localhost";
            }

            var queuename = "dcc.multi.clustering." + Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant();


            string url = string.Format("rabbitmq://{0}/{1}", server, queuename);

            if (!isCluster)
            {
                return url;
            }
            return url + "?ha=true";
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
