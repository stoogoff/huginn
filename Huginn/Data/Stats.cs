using Newtonsoft.Json;

namespace Huginn.Data {
	public class Stats: CouchData {
		[JsonProperty("article")]
		public string Chapter { get; set; }

		[JsonProperty("wordcount")]
		public int WordCount { get; set; }

		[JsonProperty("doc_type")]
		public string DocType {
			get {
				return "Stats";
			}
		}

		public static Stats CreateFromChapter(Chapter chapter, int wordCount) {
			return new Stats {
				Chapter = chapter.Id,
				Author = chapter.Author,
				WordCount = wordCount
			};
		}

		public static Stats CreateFromChapter(Chapter chapter) {
			return CreateFromChapter(chapter, chapter.WordCount);
		}
	}
}

