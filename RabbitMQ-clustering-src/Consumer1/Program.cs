using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Consumer.Shared;

using MassTransit;
using MassTransit.Log4NetIntegration;

namespace Consumer1
{
    class Program
    {
        private static void Main(string[] args)
        {
            ConsumerRunner runner = new ConsumerRunner();
            runner.Run();

        }
    }
}
