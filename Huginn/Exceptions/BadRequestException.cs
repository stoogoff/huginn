using Nancy;

namespace Huginn.Exceptions {
	public class BadRequestException: ServiceException {
		public BadRequestException(string message): base(HttpStatusCode.BadRequest, message) { }
		public BadRequestException(): this("Malformed request") { }
	}
}

