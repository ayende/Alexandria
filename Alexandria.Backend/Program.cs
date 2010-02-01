using System;
using Rhino.ServiceBus.Hosting;

namespace Alexandria.Backend
{
	class Program
	{
		static void Main(string[] args)
		{
			var host = new RemoteAppDomainHost(typeof (AlexandriaBootStrapper));
			host.Start();

			Console.WriteLine("Starting to process messages");
			Console.ReadLine();
		}
	}
}
