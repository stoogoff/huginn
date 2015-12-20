using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;

namespace Huginn {
	using Huginn.Couch;
	using Huginn.Exceptions;
	using Huginn.Models;

	public class HuginnBootstrapper: DefaultNancyBootstrapper {
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
			base.ApplicationStartup(container, pipelines);

			// get authentication information
			pipelines.BeforeRequest += context => {
				/*var user = new HuginnUser {
					AuthorId = 2,
					UserName = "storytella",
					Claims = new List<string>()
				};

				context.CurrentUser = user;

				return null;*/

				var auth = context.Request.Headers.Authorization;

				if(string.IsNullOrEmpty(auth)) {
					throw ServiceException.Unauthorised();
				}

				auth = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Replace("Basic ", string.Empty)));

				var components = auth.Split(new [] { ':' });

				if(components.Length < 2) {
					throw ServiceException.BadRequest();
				}

				int author;
				int.TryParse(components[0], out author);

				var user = new HuginnUser {
					AuthorId = author,
					UserName = components[1],
					Claims = new List<string>()
				};

				context.CurrentUser = user;

				return null;
			};

			// handle all exceptions and return a JSON response
			pipelines.OnError += (context, exception) => {
				Console.WriteLine(exception);

				if(exception is ServiceException) {
					var serviceException = exception as ServiceException;

					return new Negotiator(context)
						.WithStatusCode(serviceException.StatusCode)
						.WithModel(new ErrorViewModel(serviceException, context.Request));
				}

				return new Negotiator(context)
					.WithStatusCode(HttpStatusCode.InternalServerError)
					.WithModel(exception.Message);
			};

			// set up couch and register interface -> implementation mappings
			var host = ConfigurationManager.AppSettings["CouchHost"] ?? "localhost";
			var port = ConfigurationManager.AppSettings["CouchPort"] ?? "5984";
			var database = ConfigurationManager.AppSettings["CouchDatabase"];

			container.Register<ICouchClient, CouchClient>(new CouchClient(host, port, database));
		}
	}
}

