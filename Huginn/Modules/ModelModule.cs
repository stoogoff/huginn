using Nancy;
using Nancy.ModelBinding;

namespace Huginn.Modules {
	using Huginn.Data;
	using Huginn.Models;
	using Huginn.Services;
	using Huginn.Exceptions;
	using Huginn.Json;

	public abstract class ModelModule<T,S>: NancyModule where S: CouchData where T: ViewModel<S> {
		protected IModelViewService<T,S> service; // This **must** be set by inheriting classes

		protected ModelModule(IModelViewService<T,S> service, string basePath): base(basePath) {
			this.service = service;

			Before += context => {
				var user = (context.CurrentUser as HuginnUser);

				if(user != null) {
					service.AuthorId = user.AuthorId;
					return null;
				}

				throw ServiceException.Unauthorised();
			};

			// index of T
			Get["/"] = parameters => service.All();

			/*Post["/"] = parameters => {
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, service.Create(model));
			};*/

			// single T based on ID
			Get["/{id}"] = parameters => {
				var id = parameters.id.ToString();

				return service.Get(id);
			};

			/*Put["/{id}"] = parameters => {
				var id = parameters.id.ToString();
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, service.Save(id, model));
			};*/

			/*Delete["/{id}/revision/{revision}"] = parameters => {
				var id = parameters.id.ToString();
				var revision = parameters.revision.ToString();

				// TODO should this be something other than HttpStatusCode.Created???
				return ResponseHandler.GetResponse(HttpStatusCode.Created, service.Delete(id, revision));
			};*/

			// TODO versions
		}
	}
}

