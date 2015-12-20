using System.Xml.Serialization;
using Newtonsoft.Json;
using Nancy;

namespace Huginn.Models {
	using Huginn.Exceptions;

	[XmlRootAttribute("error")]
	public class ErrorViewModel {
		protected ServiceException exception;
		protected ErrorRequest request;

		public ErrorViewModel(ServiceException exception, Request request) {
			this.exception = exception;
			this.request = new ErrorRequest(request);
		}

		[JsonProperty("error_code")]
		[XmlElement("error-code")]
		public int ErrorCode {
			get {
				return (int) exception.StatusCode;
			}
		}

		[JsonProperty("error")]
		[XmlElement("error")]
		public string Error {
			get {
				return exception.StatusCode.ToString();
			}
		}

		[JsonProperty("request")]
		[XmlElement("request")]
		public ErrorRequest Request {
			get {
				return request;
			}
		}

		[JsonProperty("message")]
		[XmlElement("message")]
		public string Message {
			get {
				return exception.Message;
			}
		}

		[JsonProperty("object")]
		[XmlIgnore]
		public string Object {
			get {
				return "error";
			}
		}
	}

	public class ErrorRequest {
		protected Request request;

		public ErrorRequest(Request request) {
			this.request = request;
		}

		[JsonProperty("url")]
		[XmlElement("url")]
		public string Url {
			get {
				return request.Url;
			}
		}

		[JsonProperty("method")]
		[XmlElement("method")]
		public string Method {
			get {
				return request.Method;
			}
		}
	}
}

