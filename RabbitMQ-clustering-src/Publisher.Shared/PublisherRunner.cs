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
        private readonly bool _noInterative;

        private readonly string _publisherUri;

        private string _PublisherName;

        public PublisherRunner(string[] args)
        {
            this._publisherUri = GetRabittMqUrl();
            this._noInterative =  args.Any(arg => arg.TrimStart('/').TrimStart('-').Equals("ni", StringComparison.OrdinalIgnoreCase));;
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

            if (this._noInterative)
            {
               
                    this.SendEvery500Ms();
             
            }

            this.ConsoleInteractive();
        }

        private void SendEvery500Ms()
        {
            while (true)
            {
                Bus.Instance.Publish(
                    new ArticleUpdateMessage
                    {
                        ThrowError = "",
                        ArticleId = "dmf1031545645",
                        CreationDate = DateTime.Now,
                        PublisherName = this._PublisherName
                    });
                Thread.Sleep(500);
            }
        }

        private void ConsoleInteractive()
        {
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

                Bus.Instance.Publish(
                    new ArticleUpdateMessage
                    {
                        ThrowError = throwErrorOn,
                        ArticleId = "dmf1031545645",
                        CreationDate = DateTime.Now,
                        PublisherName = this._PublisherName
                    });
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

            string queuename = "dcc.multi." + Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant();

            string url = string.Format("rabbitmq://{0}/{1}", server, queuename);

            if (!isCluster)
            {
                return url;
            }
            return url + "?ha=true";
        }
    }
}