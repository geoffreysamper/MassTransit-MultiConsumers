using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;

using log4net.Config;

using MassTransit;
using MassTransit.Log4NetIntegration;

using Messages;

namespace Publisher.Shared
{
    public class PublisherRunner
    {
        private readonly string _publisherUri;

        private string _PublisherName;

        public PublisherRunner()
        {
            this._publisherUri = GetRabittMqUrl();
        }

        public void Run()
        {
            XmlConfigurator.Configure();
            Console.Title = Assembly.GetEntryAssembly().GetName().Name;
            Bus.Initialize(
                sbc =>
                {
                    sbc.UseLog4Net();
                    sbc.UseRabbitMq();
                    sbc.ReceiveFrom(this._publisherUri);
                    sbc.Subscribe(subs => subs.Handler<ArticleUpdateMessage>(msg => Console.WriteLine(msg.ArticleId + "  " + msg.CreationDate)));
                });


            this.SendEvery500Ms();


        }

        private void SendEvery500Ms()
        {
            while (true)
            {
                try
                {
                    var message = new ArticleUpdateMessage
                    {
                        ThrowError = "",
                        ArticleId = "dmf1031545645",
                        CreationDate = DateTime.Now,
                        PublisherName = this._PublisherName
                    };

                    Bus.Instance.Publish(message);
                    Thread.Sleep(500);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("failed to publish message {0}. Enter sleep mode for 1sec", ex.ToString());
                    Thread.Sleep(1000);
                }

            }
        }

        public static string GetRabittMqUrl()
        {
            bool isCluster = false;

            bool.TryParse(ConfigurationManager.AppSettings["rabbitserverIsCluster"], out isCluster);

            string server = ConfigurationManager.AppSettings["rabbitserver"];

            if (string.IsNullOrEmpty(server))
            {
                server = "localhost";
            }

            string queuename = "dcc.multi.clustering." + Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant();

            string url = string.Format("rabbitmq://{0}/{1}", server, queuename);

            if (!isCluster)
            {
                return url;
            }
            return url + "?ha=true";
        }
    }
}