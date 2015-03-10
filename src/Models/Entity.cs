using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Huginn.Models {
	public class Entity: BaseModel {
		[JsonProperty("hint")]
		public string Hint { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("novels")]
		public IList<string> Novels { get; set; }

		[JsonProperty("entities")]
		public IDictionary<string, string> Entities { get; set; }
	}
}

