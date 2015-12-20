using System;
using Nancy;
using Nancy.Responses;

namespace Huginn.Exceptions {
	using Huginn.Models;
	
	public static class ExceptionHandler {
		/*public static Response GetResponse(Exception exception) {
			var response = new JsonResponse<Exception>(exception, new JsonNetSerialiser());

			response.StatusCode = HttpStatusCode.InternalServerError;

			return response;
		}*/

		/*public static Response GetResponse(ServiceException exception) {
			return new ErrorViewModel(exception);
		}*/
	}
}
