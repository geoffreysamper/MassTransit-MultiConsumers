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

          if (message.ThrowError.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1 || message.ThrowError.Equals("all", StringComparison.OrdinalIgnoreCase))
          {
              throw new Exception("Failed to proccess message for on client " + name );
          }



          Console.WriteLine(" {0} articleupdate {1} {2}", name.ToUpperInvariant(), message.ArticleId, message.CreationDate);
      }
    }
}
