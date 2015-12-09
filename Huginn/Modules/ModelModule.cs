using Nancy;
using Nancy.ModelBinding;

namespace Huginn.Modules {
	using Huginn.Exceptions;
	using Huginn.Managers;
	using Huginn.Json;

	public abstract class ModelModule<T>: NancyModule where T: Huginn.Data.CouchData {
		protected DataManager<T> manager; // This **must** be set by inheriting classes

		protected ModelModule(string basePath): base(basePath) {
			Before += context => {
				var user = (context.CurrentUser as HuginnUser);

				if(user != null) {
					manager.AuthorId = user.AuthorId;
					return null;
				}

				return ResponseHandler.GetResponse(new UnauthorisedException());
			};

			// index of T
			Get["/"] = parameters => manager.All();

			Post["/"] = parameters => {
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, manager.Create(model));
			};

			// single T based on ID
			Get["/{id}"] = parameters => {
				var id = parameters.id.ToString();

				return manager.Get(id);
			};

			Put["/{id}"] = parameters => {
				var id = parameters.id.ToString();
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, manager.Save(id, model));
			};

			Delete["/{id}/revision/{revision}"] = parameters => {
				var id = parameters.id.ToString();
				var revision = parameters.revision.ToString();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, manager.Delete(id, revision));
			};

			// TODO versions
		}
	}
}

