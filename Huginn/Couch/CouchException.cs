using System;
using System.Net;

namespace Huginn.Couch {
	public class CouchException: Exception {
		public CouchException(HttpStatusCode statusCode, string message): base(message) {
			StatusCode = statusCode;
		}

		public CouchException(HttpStatusCode statusCode, Uri url, string message): this(statusCode, message) {
			Url = url;
		}

		/// <summary>
		/// The status code of the exception.
		/// </summary>
		/// <value>The status code.</value>
		public HttpStatusCode StatusCode { get; protected set; }

		/// <summary>
		/// The URL of the request which caused the exception.
		/// </summary>
		/// <value>The URL.</value>
		public Uri Url { get; protected set; }
	}
}

