using System;
using Newtonsoft.Json;

namespace Huginn.Models {
	public class Chapter: BaseModel {
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonProperty("novel")]
		public string Novel { get; set; }

		[JsonProperty("sort")]
		public int Sort { get; set; }
	}
}

