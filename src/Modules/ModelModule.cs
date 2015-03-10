using System;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Huginn.Couch;
using Huginn.Managers;

namespace Huginn.Modules {
	public abstract class ModelModule<T>: NancyModule where T: Huginn.Models.BaseModel {
		//private int userId = 2;
		protected BaseManager<T> manager;

		public ModelModule(string basePath): base(basePath) {
			/*Before += context => {
				// TODO retrieve user id from request header

				return null;
			};*/

			// index of T
			Get["/"] = parameters => manager.All();

			Post["/"] = parameters => {
				var model = this.Bind<T>();

				return manager.Save(model);
			};

			// single T based on ID
			Get["/{id}"] = parameters => {
				var id = parameters.id.ToString();

				return manager.Get(id);
			};

			Put["/{id}"] = parameters => {
				var id = parameters.id.ToString();
				var model = this.Bind<T>();

				return manager.Save(id, model);
			};

			Delete["/{id}/{revision}"] = parameters => {
				var id = parameters.id.ToString();
				var revision = parameters.revision.ToString();

				return manager.Delete(id, revision);
			};

			// TODO versions
		}
	}
}

