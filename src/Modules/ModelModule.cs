using System;
using Nancy;
using Nancy.ModelBinding;
using Huginn.Managers;
using Huginn.Json;

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
			Get["/"] = parameters => ResponseHandler.GetResponse(manager.All());

			Post["/"] = parameters => {
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, manager.Create(model));
			};

			// single T based on ID
			Get["/{id}"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse(manager.Get(id));
			};

			Put["/{id}"] = parameters => {
				var id = parameters.id.ToString();
				var model = this.Bind<T>();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, manager.Save(id, model));
			};

			Delete["/{id}/{revision}"] = parameters => {
				var id = parameters.id.ToString();
				var revision = parameters.revision.ToString();

				return ResponseHandler.GetResponse(HttpStatusCode.Created, manager.Delete(id, revision));
			};

			// TODO versions
		}
	}
}

