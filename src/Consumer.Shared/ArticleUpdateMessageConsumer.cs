using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using MassTransit;

using Messages;

namespace Consumer.Shared
{
  public  class ArticleUpdateMessageConsumer :  Consumes<ArticleUpdateMessage>.All
    {
      public void Consume(ArticleUpdateMessage message)
      {
          string name = Assembly.GetEntryAssembly().GetName().Name;
          Console.WriteLine(" {0} articleupdate {1} {2}", name.ToUpperInvariant(), message.ArticleId, message.CreationDate);
      }
    }
}
