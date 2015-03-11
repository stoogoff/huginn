using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Huginn.Data {
	public class Entity: BaseData {
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

