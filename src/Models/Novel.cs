using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huginn.Models {
	public class Novel: BaseModel {
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("synopsis")]
		public string Synopsis { get; set; }

		[JsonProperty("contributors")]
		public IList<string> Contributors { get; set; }

		[JsonProperty("include_license")]
		public bool IncludeLicense { get; set; }

		[JsonProperty("archive")]
		public bool Archive { get; set; }
	}
}

