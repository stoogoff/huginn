using System;
using Newtonsoft.Json;

namespace Huginn.Models {
	public class Test {
		[JsonProperty("_id")]
		public string Id { get; set; }

		[JsonProperty("_rev")]
		public string Revision { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("doc_type")]
		public string DocType {
			get {
				return "test";
			}
		}
	}
}
