using System;
using System.Net;
using RestSharp;
using Newtonsoft.Json;

namespace Huginn.Couch {
	using Huginn.Exceptions;
	using Huginn.Json;

	public class CouchClient: ICouchClient {
		public CouchClient(string database): this("localhost", "5984", database) { }

		public CouchClient(string host, string port, string database) {
			Host = host;
			Port = port;
			Database = database;
		}

		public string Host { get; protected set; }
		public string Port { get; protected set; }
		public string Database { get; protected set; }

		protected string BaseUrl {
			get {
				return string.Format("http://{0}:{1}/", Host, Port);
			}
		}

		protected string DatabaseUrl {
			get {
				return string.Format("http://{0}:{1}/{2}/", Host, Port, Database);
			}
		}

		#region Single object crud methods
		public T GetDocument<T>(string id) {
			try {
				return Execute<T>(new RestRequest(id, Method.GET));
			}
			catch(CouchException) {
				return default(T);
			}
		}

		public CouchResponse Save(object model) {
			return Save(NextId(), model);
		}

		public CouchResponse Save(string id, object model) {
			if(id == null) {
				id = NextId();
			}

			var json = JsonConvert.SerializeObject(model, Formatting.Indented, new SerialiserSettings());
			var request = new RestRequest(id, Method.PUT);

			request.AddParameter("application/json", json, ParameterType.RequestBody);

			return Execute<CouchResponse>(request);
		}

		public CouchResponse Delete(string id, string revision) {
			var request = new RestRequest(id, Method.DELETE);

			request.AddParameter("rev", revision);

			return Execute<CouchResponse>(request);
		}
		#endregion

		public string NextId() {
			var response = Execute<Uuid>(BaseUrl, new RestRequest("_uuids", Method.GET));

			return response.Uuids[0];
		}

		#region Views
		public ViewResult<T> GetView<T>(string designDoc, string view) {
			var request = new RestRequest(string.Format("_design/{0}/_view/{1}", designDoc, view), Method.GET);

			return Execute<ViewResult<T>>(request);
		}

		public ViewResult<T> GetView<T>(string designDoc, string view, ViewQuery query) {
			var request = new RestRequest(string.Format("_design/{0}/_view/{1}?{2}", designDoc, view, query), Method.GET);

			return Execute<ViewResult<T>>(request);
		}

		public ViewResult GetView(string designDoc, string view) {
			var request = new RestRequest(string.Format("_design/{0}/_view/{1}", designDoc, view), Method.GET);

			return Execute<ViewResult>(request);
		}

		public ViewResult GetView(string designDoc, string view, ViewQuery query) {
			var request = new RestRequest(string.Format("_design/{0}/_view/{1}?{2}", designDoc, view, query), Method.GET);

			return Execute<ViewResult>(request);
		}
		#endregion

		protected T Execute<T>(string url, RestRequest request) {
			var client = new RestClient(url);
			var response = client.Execute(request);

			#if DEBUG
			Console.WriteLine("{0} - {1} - {2}{3} - {4}", DateTime.UtcNow, request.Method, url, request.Resource, (int) response.StatusCode);
			#endif

			if(response.StatusCode == HttpStatusCode.NotFound) {
				throw new CouchException(HttpStatusCode.NotFound, new Uri(url + request.Resource), "Object not found.");
			}

			if(response.ErrorException != null) {
				throw new CouchException(response.StatusCode, response.ErrorMessage);
			}

			return JsonConvert.DeserializeObject<T>(response.Content);
		}

		protected T Execute<T>(RestRequest request) {
			return Execute<T>(DatabaseUrl, request);
		}
	}
}

