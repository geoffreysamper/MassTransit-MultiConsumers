using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

using Consumer.Shared;

using MassTransit;
using MassTransit.Log4NetIntegration;

namespace Consumer1
{
    class Program
    {
        static AutoResetEvent _quitEvent = new AutoResetEvent(false);
        private static void Main(string[] args)
        {
            Console.CancelKeyPress +=((sender, e) => _quitEvent.Set());
            Console.WriteLine("PRESS (Ctrl+C or Ctrl+Break) to quit");


            ConsumerRunner runner = new ConsumerRunner();
            runner.Run();


            _quitEvent.WaitOne();

        }
    }
}
