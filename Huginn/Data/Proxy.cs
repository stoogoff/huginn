using System;
using Newtonsoft.Json;

namespace Huginn.Data {
	public class Proxy: CouchData {
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("doc_type")]
		public string DocType { get; set; }
	}
}

