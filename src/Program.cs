using System;
using System.Threading;
using System.Linq;
using Nancy;
using Nancy.Hosting.Self;

namespace monotest {
	public class Program {
		public static void Main(string[] args) {
			StaticConfiguration.DisableErrorTraces = false;

			var uri = "http://localhost:8888";
			Console.WriteLine(uri);

			var host = new NancyHost(new Uri(uri));

			host.Start();

			if(args.Any(s => s.Equals("-d", StringComparison.CurrentCultureIgnoreCase))) {
				Thread.Sleep(Timeout.Infinite);
			}
			else {
				Console.ReadKey();
			}

			host.Stop();
		}
	}
}

