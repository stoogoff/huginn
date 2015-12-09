﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huginn.Data {
	public class Book: CouchData {
		[JsonProperty("doc_type")]
		public string DocType {
			get {
				return "Novel";
			}
		}

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("synopsis")]
		public string Synopsis { get; set; }

		[JsonProperty("publisher")]
		public string Publisher { get; set; }

		[JsonProperty("contributors")]
		public IList<string> Contributors { get; set; }

		[JsonProperty("include_license")]
		public bool? IncludeLicense { get; set; }

		[JsonProperty("include_synopsis")]
		public bool? IncludeSynopsis { get; set; }

		[JsonProperty("archive")]
		public bool Archive { get; set; }

		[JsonProperty("image")]
		public int? Image { get; set; }
	}
}
