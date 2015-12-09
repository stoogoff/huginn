using System;
using Nancy;

namespace Huginn.Exceptions {
	public class ServiceException: ApplicationException {
		public ServiceException(HttpStatusCode status, string message, Exception innerException): base(message, innerException) {
			StatusCode = status;
		}
		public ServiceException(System.Net.HttpStatusCode status, string message, Exception innerException): base(message, innerException) {
			//int tmpStatus = (int) status;

			StatusCode = (HttpStatusCode) Enum.Parse(typeof(HttpStatusCode), status.ToString());
		}
		public ServiceException(HttpStatusCode status, string message): this(status, null, message) {

		}
		public ServiceException(HttpStatusCode status, Uri resource, string message): base(message) {
			StatusCode = status;
			Resource = resource;
		}

		public HttpStatusCode StatusCode { get; set; }
		public Uri Resource { get; set; }

		// TODO output for this should be simpler:
		/*
			{
				"status_code": 401,
				"message": "Unauthorized",
			}
		*/
	}
}

