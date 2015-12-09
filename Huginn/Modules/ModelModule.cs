using Nancy;
using Nancy.ModelBinding;

namespace Huginn.Modules {
	using Huginn.Services;
	using Huginn.Exceptions;
	using Huginn.Managers;
	using Huginn.Json;

	public abstract class ModelModule<T>: NancyModule where T: Huginn.Models.ViewModel {
		protected IModelViewService<T> service; // This **must** be set by inheriting classes

		protected ModelModule(string basePath): base(basePath) {
			Before += context => {
				var user = (context.CurrentUser as HuginnUser);

				if(user != null) {
					service.AuthorId = user.AuthorId;
					return null;
				}

				return ResponseHandler.GetResponse(new UnauthorisedException());
			};

			// index of T
			Get["/"] = parameters => service.All();

			Post["/"] = parameters => {
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, service.Create(model));
			};

			// single T based on ID
			Get["/{id}"] = parameters => {
				var id = parameters.id.ToString();

				return service.Get(id);
			};

			Put["/{id}"] = parameters => {
				var id = parameters.id.ToString();
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, service.Save(id, model));
			};

			Delete["/{id}/revision/{revision}"] = parameters => {
				var id = parameters.id.ToString();
				var revision = parameters.revision.ToString();

				// TODO should this be something other than HttpStatusCode.Created???
				return ResponseHandler.GetResponse(HttpStatusCode.Created, service.Delete(id, revision));
			};

			// TODO versions
		}
	}
}

