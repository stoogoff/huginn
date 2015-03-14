using System;
using Newtonsoft.Json;

namespace Huginn.Data {
	public class Stats: CouchData {
		[JsonProperty("article")]
		public string Article { get; set; }

		[JsonProperty("wordcount")]
		public int WordCount { get; set; }

		[JsonProperty("doc_type")]
		public string DocType {
			get {
				return "Stats";
			}
		}
	}
}

