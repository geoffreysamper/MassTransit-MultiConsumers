using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using MassTransit;

using Messages;

namespace Messages
{
    public class ArticleUpdateMessage
    {
        public string ArticleId { get; set; }

    }
}


namespace Consumer.Shared
{


  public  class OldArticleUpdateMessageConsumer :  Consumes<ArticleUpdateMessage>.All
    {
      public void Consume(ArticleUpdateMessage message)
      {
          string name = Assembly.GetEntryAssembly().GetName().Name;
          Console.WriteLine(" {0} articleupdate {1}", name.ToUpperInvariant(), message.ArticleId);
      }
    }
}
