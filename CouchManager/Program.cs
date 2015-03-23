using System;
using System.IO;
using System.Configuration;
using Huginn.Couch;
using Huginn.Exceptions;
using CouchManager.Data;

namespace CouchManager {
	public class Program {
		public static void Main(string[] args) {
			// get settings
			var host = ConfigurationManager.AppSettings["CouchHost"] ?? "localhost";
			var port = ConfigurationManager.AppSettings["CouchPort"] ?? "5984";
			var database = ConfigurationManager.AppSettings["CouchDatabase"];

			var client = new CouchClient(host, port, database);
			var root = ConfigurationManager.AppSettings["DesignPath"];

			Console.WriteLine("Reading folder: {0}", root);

			// create objects and save
			foreach(var folder in Directory.GetDirectories(root)) {
				var design = Design.FromFolder(folder);

				try {
					var current = client.GetDocument<Design>(design.Id);

					current.ClearViews();
					current.AddViews(design.Views);

					client.Save(current.Id, current);
				}
				catch(ObjectNotFoundException) {
					client.Save(design.Id, design);
				}
			}
		}
	}
}

