using System;
using Nancy;

namespace Huginn.Exceptions {
	public class ObjectNotFoundException: ServiceException {
		public ObjectNotFoundException(Uri resource, string message): base(HttpStatusCode.NotFound, resource, message) {

		}
		public ObjectNotFoundException(Uri resource): this(resource, "Object not found") {

		}
	}
}

