using System;
using System.Collections.Generic;
using System.Configuration;

namespace Huginn.Services {
	using Huginn.Couch;
	using Huginn.Data;

	public class DataRepository {
		private readonly CouchClient client;

		public DataRepository(int authorId) {
			var host = ConfigurationManager.AppSettings["CouchHost"] ?? "localhost";
			var port = ConfigurationManager.AppSettings["CouchPort"] ?? "5984";
			var database = ConfigurationManager.AppSettings["CouchDatabase"];

			client = new CouchClient(host, port, database);
			AuthorId = authorId;
		}

		public CouchClient Client {
			get {
				return client;
			}
		}

		public int AuthorId { get; protected set; }

		/*public IList<T> AllObjects<T>() {
			return AllObjects<T>(view);
		}*/

		public IList<T> AllObjects<T>(string view) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};
			var response = Client.GetView<T>(view, "by_author", query);

			return response.ToList();
		}

		public T GetObject<T>(string id) {
			return Client.GetDocument<T>(id);
		}

		// save a new object
		public T CreateObject<T>(T model) where T: CouchData  {
			model.Modified = model.Created = DateTime.UtcNow;

			var response = Client.Save(model);

			return GetObject<T>(response.Id);
		}

		// save an existing object
		public T SaveObject<T>(string id, T model) where T: CouchData {
			model.Modified = DateTime.UtcNow;

			var response = Client.Save(id, model);

			return GetObject<T>(response.Id);
		}

		// delete an existing object
		public bool DeleteObject(string id, string revision) {
			var response = Client.Delete(id, revision);

			return response.Ok;
		}
	}

}

