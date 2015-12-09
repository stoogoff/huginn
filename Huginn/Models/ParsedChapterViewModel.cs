using System.Xml.Serialization;
using Newtonsoft.Json;


namespace Huginn.Models {
	// the idea here is to return a chapter which is ready for conversion to EPUB (via a Markdown converter)
	// /books/<id>/chapters      - IList<ParsedChapterViewModel>
	// /books/<id>/chapters/<id> - ParsedChapterViewModel
	[XmlRootAttribute("chapter")]
	public class ParsedChapterViewModel: ViewModel {
		public ParsedChapterViewModel() {
			Object = "chapter";
		}

		// full Title which has gone through the following process:
		//	- entities replaced
		//	- typography converted
		[JsonProperty("title")]
		[XmlElement("title")]
		public string Title { get; set; } // returns "Unnamed Chapter" if not set

		// full Content which has gone through the following process:
		//	- entities replaced
		//	- todos removed
		//	- typography converted
		// TODO this should probably be a CData section
		[JsonProperty("markdown_content")]
		[XmlElement("markdown-content")]
		public string MarkdownContent { get; set; }

		[JsonProperty("sort")]
		[XmlElement("sort")]
		public int Sort { get; set; }

		[JsonProperty("word_count")]
		[XmlElement("word-count")]
		public int WordCount { get; set; }

		// handle via Link header
		// This could also be achieved using a Link header
		// see http://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api#pagination
		// ID of next chapter or null (or Proxy instance?)
		//public string NextChapter { get; set; }

		// ID of previous chapter or null (or Proxy instance?)
		//public string PreviousChapter { get; set; }
	}
}

