using System;
using System.Text;
using System.Collections.Generic;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy.Json;
using Huginn.Json;
using Huginn.Exceptions;

namespace Huginn {
	public class HuginnBootstrapper: DefaultNancyBootstrapper {
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
			base.ApplicationStartup(container, pipelines);

			// get authentication information
			pipelines.BeforeRequest += context => {
				var user = new HuginnUser {
					AuthorId = 2,
					UserName = "storytella",
					Claims = new List<string>()
				};

				context.CurrentUser = user;

				return null;

				/*var auth = context.Request.Headers.Authorization;

				if(string.IsNullOrEmpty(auth)) {
					return ResponseHandler.GetResponse(new UnauthorisedException());
				}

				try {
					auth = Encoding.UTF8.GetString(Convert.FromBase64String(auth.Replace("Basic ", string.Empty)));

					var components = auth.Split(new [] { ':' });

					if(components.Length < 2) {
						throw new UnauthorisedException();
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
				}
				catch(FormatException fex) {
					return ResponseHandler.GetResponse(new UnauthorisedException(fex));
				}
				catch(ServiceException se) {
					return ResponseHandler.GetResponse(se);
				}
				catch(Exception ex) {
					return ResponseHandler.GetResponse(ex);
				}*/
			};

			// handle all exceptions and return a JSON response
			pipelines.OnError += (ctx, ex) => {
				Console.WriteLine(ex);

				if(ex is ServiceException) {
					return ResponseHandler.GetResponse(ex as ServiceException);
				}
				else {
					return ResponseHandler.GetResponse(ex);
				}
			};

			// set up custom DateTime converter for JSON serialisation
			JsonSettings.PrimitiveConverters.Add(new DateTimeConverter());
		}
	}
}

