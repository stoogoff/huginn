using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huginn.Data {
	public class Chapter: BaseData {
		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonIgnore]
		public string RawContent { get; set; }

		[JsonProperty("novel")]
		public string Novel { get; set; }

		[JsonProperty("sort")]
		public int Sort { get; set; }

		[JsonIgnore]
		public string NextSibling { get; set; }

		// TODO redis cache
		[JsonIgnore]
		public int WordCount {
			get {
				var total = Content.Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

				return total.Length;
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
			RawContent = Content;

			return this;
		}
	}
}

