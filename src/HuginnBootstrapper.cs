using System;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Nancy.Json;
using Huginn.Exceptions;

namespace Huginn {
	public class HuginnBootstrapper: DefaultNancyBootstrapper {
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
			base.ApplicationStartup(container, pipelines);

			pipelines.OnError += (ctx, ex) => {
				if(ex is ServiceException) {
					return GetResponse(ex as ServiceException);
				}
				else {
					return GetResponse(ex);
				}
			};

			JsonSettings.PrimitiveConverters.Add(new Huginn.Json.DateTimeConverter());
		}

		protected Response GetResponse(Exception exception) {
			var response = new JsonResponse<Exception>(exception, new DefaultJsonSerializer());

			response.StatusCode = HttpStatusCode.InternalServerError;

			return response;
		}

		protected Response GetResponse(ServiceException exception) {
			var response = new JsonResponse<ServiceException>(exception, new DefaultJsonSerializer());

			response.StatusCode = exception.StatusCode;

			return response;
		}
	}
}

