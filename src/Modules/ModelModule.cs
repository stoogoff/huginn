using System;
using Nancy;
using Nancy.ModelBinding;
using Huginn.Couch;
using Newtonsoft.Json;

namespace Huginn.Modules {
	public abstract class ModelModule<T>: NancyModule {
		//private int userId = 2;

		public ModelModule(string basePath): base(basePath) {
			/*Before += context => {
				// TODO retrieve user id from request header

				return null;
			};*/

			// index of T
			/*
			Get["/"] = parameters => {

			};*/

			Post["/"] = parameters => {
				var client = GetClient();
				var model = this.Bind<T>();

				return client.Save(model);
			};

			// single T based on ID
			Get["/{id}"] = parameters => {
				var id = parameters.id.ToString();
				var client = GetClient();

				return client.Get<T>(id);
			};

			Put["/{id}"] = parameters => {
				var id = parameters.id.ToString();
				var client = GetClient();
				var model = this.Bind<T>();

				return client.Save(id, model);
			};

			Delete["/{id}/{revision}"] = parameters => {
				var id = parameters.id.ToString();
				var revision = parameters.revision.ToString();
				var client = GetClient();

				return client.Delete(id, revision);
			};

			// TODO versions
		}

		protected CouchClient GetClient() {
			return new CouchClient("test");
		}
	}
}

