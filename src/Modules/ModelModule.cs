using System;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Huginn.Managers;
using Huginn.Exceptions;

namespace Huginn.Modules {
	public abstract class ModelModule<T>: NancyModule where T: Huginn.Data.BaseData {
		protected DataManager<T> manager; // This **must** be set by inheriting classes

		protected ModelModule(string basePath): base(basePath) {
			Before += context => {
				manager.AuthorId = 2;
				return null;
				// This may be better as an Authorization header
				/*var authors = context.Request.Headers["X-AUTHOR"];

				foreach(var author in authors) {
					int authorId;

					if(int.TryParse(author, out authorId)) {
						manager.AuthorId = authorId;
						break;
					}
				}

				return manager.AuthorId == 0 ? GetResponse(new UnauthorisedException()) : null;*/
			};

			// index of T
			Get["/"] = parameters => {
				try {
					return GetResponse(manager.All());
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};

			Post["/"] = parameters => {
				try {
					var model = this.Bind<T>();

					return GetResponse(HttpStatusCode.Created, manager.Create(model));
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};

			// single T based on ID
			Get["/{id}"] = parameters => {
				try {
					var id = parameters.id.ToString();

					return GetResponse(manager.Get(id));
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};

			Put["/{id}"] = parameters => {
				try {
					var id = parameters.id.ToString();
					var model = this.Bind<T>();

					return GetResponse(HttpStatusCode.Created, manager.Save(id, model));
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};

			Delete["/{id}/{revision}"] = parameters => {
				try {
					var id = parameters.id.ToString();
					var revision = parameters.revision.ToString();

					return GetResponse(HttpStatusCode.Created, manager.Delete(id, revision));
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};

			// TODO versions
		}

		protected Response GetResponse(object model) {
			return GetResponse(HttpStatusCode.OK, model);
		}

		protected Response GetResponse(HttpStatusCode status, object model) {
			var response = new JsonResponse(model, new DefaultJsonSerializer());

			response.StatusCode = status;

			return response;
		}

		protected Response GetResponse(ServiceException exception) {
			var response = new JsonResponse<ServiceException>(exception, new DefaultJsonSerializer());

			response.StatusCode = exception.StatusCode;

			return response;
		}
	}
}

