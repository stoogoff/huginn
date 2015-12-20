using System;
using Nancy;

namespace Huginn.Exceptions {
	public class ObjectNotFoundException: ServiceException {
		public ObjectNotFoundException(string message): base(HttpStatusCode.NotFound, message) { }
		public ObjectNotFoundException(): this("Object with not found.") { }
	}
}

