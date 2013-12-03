using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MassTransit;
using MassTransit.Log4NetIntegration;

using Messages;

using Publisher.Shared;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            PublisherRunner publisherRunner = new PublisherRunner();
            publisherRunner.Run();
        }


        

    }
}
