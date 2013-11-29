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
            ConsumerRunner runner = new ConsumerRunner();
            runner.Run();

        }
    }
}
