using System;
using Nancy;
using Nancy.Responses;

namespace Huginn.Json {
	using Huginn.Exceptions;

	public static class ResponseHandler {
		/*public static Response GetResponse(object model) {
			return GetResponse(HttpStatusCode.OK, model);
		}*/

		public static Response GetResponse(HttpStatusCode status, object model) {
			var response = new JsonResponse(model, new DefaultJsonSerializer());

			response.StatusCode = status;

			return response;
		}

		public static Response GetResponse(Exception exception) {
			var response = new JsonResponse<Exception>(exception, new DefaultJsonSerializer());

			response.StatusCode = HttpStatusCode.InternalServerError;

			return response;
		}

		public static Response GetResponse(ServiceException exception) {
			var response = new JsonResponse<ServiceException>(exception, new DefaultJsonSerializer());

			response.StatusCode = exception.StatusCode;

			return response;
		}
	}
}
