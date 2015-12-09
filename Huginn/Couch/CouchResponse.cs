using Newtonsoft.Json;

namespace Huginn.Couch {
	public class CouchResponse {
		[JsonProperty("ok")]
		public bool Ok { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("rev")]
		public string Revision { get; set; }
	}
}
