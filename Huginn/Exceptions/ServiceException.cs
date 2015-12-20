using System;
using Nancy;

namespace Huginn.Exceptions {
	public class ServiceException: ApplicationException {
		public const string FORBIDDEN = "You are not authorised to access resource '{0}'.";
		public const string NOT_FOUND = "Object with ID '{0}' not found.";

		public ServiceException(HttpStatusCode status, string message): base(message) {
			StatusCode = status;
		}

		public HttpStatusCode StatusCode { get; protected set; }

		public static ForbiddenException Forbidden(string id) {
			return new ForbiddenException(string.Format(FORBIDDEN, id));
		}

		public static ForbiddenException Forbidden() {
			return new ForbiddenException();
		}

		public static ObjectNotFoundException NotFound(string id) {
			return new ObjectNotFoundException(string.Format(NOT_FOUND, id));
		}

		public static ObjectNotFoundException NotFound() {
			return new ObjectNotFoundException();
		}

		public static UnauthorisedException Unauthorised() {
			return new UnauthorisedException();
		}

		public static UnauthorisedException Unauthorised(string message) {
			return new UnauthorisedException(message);
		}

		public static BadRequestException BadRequest() {
			return new BadRequestException();
		}

		public static BadRequestException BadRequest(string message) {
			return new BadRequestException(message);
		}
	}
}
