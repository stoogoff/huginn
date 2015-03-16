using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huginn.Data {
	public class Chapter: CouchData {
		[JsonProperty("doc_type")]
		public string DocType {
			get {
				return "Article";
			}
		}

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonProperty("novel")]
		public string Novel { get; set; }

		[JsonProperty("sort")]
		public int Sort { get; set; }

		[JsonIgnore]
		public string NextSibling { get; set; }

		[JsonIgnore]
		public int WordCount {
			get {
				return Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
			}
		}

		public Chapter Summarise() {
			if(Content != null) {
				var lines = Content.Split(new[] { "\n\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

				Content = lines[0] ?? string.Empty;
			}

			return this;
		}

		public Chapter ConvertEntities(IList<Entity> entities) {
			// TODO basic mustache syntax


			return this;
		}
	}
}

