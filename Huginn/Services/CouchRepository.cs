using System;
using System.Collections.Generic;
using System.Configuration;

namespace Huginn.Services {
	using Huginn.Couch;
	using Huginn.Data;

	public class CouchRepository: IDataRepository {
		private readonly ICouchClient client;

		public CouchRepository(ICouchClient couchClient) {
			client = couchClient;
		}

		public ICouchClient Client {
			get {
				return client;
			}
		}

		public int AuthorId { get; set; }

		#region Generic low-level object manipulation methods
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
		#endregion

		#region View proxy methods
		public IList<T> View<T>(string designDoc, string view) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};

			return View<T>(designDoc, view, query);
		}

		public IList<T> View<T>(string designDoc, string view, ViewQuery query) {
			return Client.GetView<T>(designDoc, view, query).ToList();
		}
		#endregion
	}
}

