﻿using System;
using System.Collections.Generic;
using Huginn.Couch;
using Huginn.Data;
using Huginn.JsonModels;

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

		public abstract IModel Create(S model);

		public abstract IModel Save(string id, S model);
		#endregion

		public bool Delete(string id, string revision) {
			var response = client.Delete(id, revision);

			return response.Ok;
		}

		protected IList<T> AllObjects<T>() {
			return AllObjects<T>(view);
		}

		protected IList<T> AllObjects<T>(string view) {
			var query = new ViewQuery {
				StartKey = "[" + AuthorId + "]",
				EndKey = "[" + AuthorId + ",{}]"
			};

			var response = client.GetView<T>(view, "by_author", query);

			return ConvertView<T>(response);
		}

		protected T GetObject<T>(string id) {
			return client.GetDocument<T>(id);
		}

		// save a new object
		protected T CreateObject<T>(T model) where T: BaseData  {
			model.Created = DateTime.Now;

			var response = client.Save(model);

			return GetObject<T>(response.Id);
		}

		// save an existing object
		protected T SaveObject<T>(string id, T model) where T: BaseData {
			model.Modified = DateTime.Now;

			var response = client.Save(id, model);

			return GetObject<T>(response.Id);
		}

		protected IList<U> ConvertView<U>(ViewResult<U> response) {
			var list = new List<U>();

			foreach(var item in response.Rows) {
				list.Add(item.Value);
			}

			return list;
		}
	}
}
