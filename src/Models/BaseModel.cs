using System;
using Newtonsoft.Json;

namespace Huginn.Models {
	public class BaseModel {
		[JsonProperty("_id")]
		public string Id { get; set; }

		[JsonProperty("_rev")]
		public string Revision { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }

		[JsonProperty("modified")]
		public DateTime Modified { get; set; }

		[JsonProperty("author")]
		public int Author { get; set; }

		[JsonProperty("doc_type")]
		public string DocType { get; set; }

		[JsonProperty("trash")]
		public bool Trash { get; set; }
	}
}

