using Nancy;

namespace Huginn.Exceptions {
	public class ForbiddenException: ServiceException {
		public ForbiddenException(string message): base(HttpStatusCode.Forbidden, message) { }
		public ForbiddenException(): this("You are not authorised to access this resource.") { }
	}
}

