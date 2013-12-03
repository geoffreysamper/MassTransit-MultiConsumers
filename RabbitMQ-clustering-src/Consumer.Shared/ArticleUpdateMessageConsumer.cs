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
      public static ConsoleColor OrginalColor = Console.ForegroundColor;
      public static  readonly object ConsoleLocker = new object();


      public void Consume(ArticleUpdateMessage message)
      {


          string name = Assembly.GetEntryAssembly().GetName().Name;

          if (message.ThrowError.IndexOf(name, StringComparison.OrdinalIgnoreCase) > -1 || message.ThrowError.Equals("all", StringComparison.OrdinalIgnoreCase))
          {
              WriteErrorToConsole(message);

              throw new Exception("Failed to proccess message for on client " + name );
          }

          Console.WriteLine(" {0}---> {1} articleupdate {2} {3}", name.ToUpperInvariant(), message.PublisherName,  message.ArticleId, message.CreationDate);
      }

      private static void WriteErrorToConsole(ArticleUpdateMessage message)
      {
          lock (ConsoleLocker)
          {
              Console.ForegroundColor = ConsoleColor.Red;
              Console.WriteLine("{0}--->  FAILED TO PROCESS MESSAGE {1} " , message.PublisherName,  message.CreationDate);
              Console.ForegroundColor = OrginalColor;
          }
      }
  }
}
