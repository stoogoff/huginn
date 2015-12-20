using System;
using Nancy;

namespace Huginn.Exceptions {
	public class UnauthorisedException: ServiceException {
		public UnauthorisedException(string message): base(HttpStatusCode.Unauthorized, message) { }
		public UnauthorisedException(): this("Authorisation not set.") { }
		public UnauthorisedException(Exception innerException): base(HttpStatusCode.Unauthorized, "Authorisation error.", innerException) { }
	}
}

