using System;
using System.Collections.Generic;
using Huginn.Couch;
using Huginn.Data;
using Huginn.Json;

namespace Huginn.Managers {
	public abstract class DataManager<S> {
		private CouchClient client;
		protected string view;

		protected DataManager(string view) {
			client = new CouchClient("muninn");

			this.view = view;
		}

		public CouchClient Client {
			get {
				return client;
			}
		}

		public int AuthorId { get; set; }

		#region Abstract methods
		public abstract IModel All();

		public abstract IModel Get(string id);

		public abstract IModel Create(S data);

		public abstract IModel Save(string id, S data);
		#endregion

		public virtual bool Delete(string id, string revision) {
			var response = client.Delete(id, revision);

			return response.Ok;
		}

		protected IList<T> AllObjects<T>() {
			return AllObjects<T>(view);
		}

		protected IList<T> AllObjects<T>(string view) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};
			var response = client.GetView<T>(view, "by_author", query);

			return response.ToList();
		}

		protected T GetObject<T>(string id) {
			return client.GetDocument<T>(id);
		}

		// save a new object
		protected T CreateObject<T>(T model) where T: CouchData  {
			model.Created = DateTime.UtcNow;

			var response = client.Save(model);

			return GetObject<T>(response.Id);
		}

		// save an existing object
		protected T SaveObject<T>(string id, T model) where T: CouchData {
			model.Modified = DateTime.UtcNow;

			var response = client.Save(id, model);

			return GetObject<T>(response.Id);
		}
	}
}

