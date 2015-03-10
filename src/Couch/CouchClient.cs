using System;
using System.Net;
using RestSharp;
using Newtonsoft.Json;

namespace Huginn.Couch {
	public class CouchClient {
		public CouchClient(string database): this("localhost", "5984", database) {

		}

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
				return string.Format("http://{0}:{1}/{2}", Host, Port, Database);
			}
		}

		#region Single object crud methods
		public T Get<T>(string id) {
			return Execute<T>(new RestRequest(id, Method.GET));
		}

		public CouchResponse Save(object model) {
			return Save(NextId(), model);
		}

		public CouchResponse Save(string id, object model) {
			if(id == null) {
				id = NextId();
			}

			var json = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
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

		protected T Execute<T>(string url, RestRequest request) {
			var client = new RestClient(url);
			var response = client.Execute(request);

			if(response.ErrorException != null) {
				throw response.ErrorException;
			}

			if(response.StatusCode == HttpStatusCode.NotFound) {
				throw new ApplicationException(string.Format("'{0}' not found.", request.Resource));
			}

			return JsonConvert.DeserializeObject<T>(response.Content);
		}

		protected T Execute<T>(RestRequest request) {
			return Execute<T>(DatabaseUrl, request);
		}
	}
}

