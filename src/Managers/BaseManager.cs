﻿using System;
using System.Collections.Generic;
using Huginn.Couch;

namespace Huginn.Managers {
	public abstract class BaseManager<T> where T: Huginn.Models.BaseModel {
		protected CouchClient client;
		protected string view;

		public BaseManager(string view) {
			client = new CouchClient("muninn");

			this.view = view;
		}

		public int AuthorId { get; set; }

		public virtual IList<T> All() {
			var query = new ViewQuery {
				StartKey = "[" + AuthorId + "]",
				EndKey = "[" + AuthorId + ",{}]"
			};

			var response = client.GetView<T>(view, "by_author", query);

			return ConvertView<T>(response);
		}

		public virtual T Get(string id) {
			// TODO handle 404
			return client.GetDocument<T>(id);
		}

		public virtual T Save(T model) {
			var response = client.Save(model);

			// TODO handle error

			return Get(response.Id);
		}

		public virtual T Save(string id, T model) {
			var response = client.Save(id, model);

			// TODO handle error

			return Get(response.Id);
		}

		public virtual bool Delete(string id, string revision) {
			var response = client.Delete(id, revision);

			return response.Ok;
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

