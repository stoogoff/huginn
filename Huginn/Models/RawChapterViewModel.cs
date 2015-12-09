using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	// the idea here is to return a chapter which is ready to be edited
	// /chapters      - IList<UnparsedChapterViewModel>
	// /chapters/<id> - UnparsedChapterViewModel
	[XmlRootAttribute("chapter")]
	public class UnparsedChapterViewModel: ViewModel {
		public UnparsedChapterViewModel() {
			Object = "chapter";
		}

		// raw chapter Title
		[JsonProperty("title")]
		[XmlElement("title")]
		public string Title { get; set; } // returns "Unnamed Chapter" if not set

		// full content as its stored in Couch
		// TODO this should be a CData section in XML
		[JsonProperty("raw_content")]
		[XmlElement("raw-content")]
		public string RawContent { get; set; }

		[JsonProperty("sort")]
		[XmlElement("sort")]
		public int Sort { get; set; }

		[JsonProperty("word_count")]
		[XmlElement("word-count")]
		public int WordCount { get; set; }
	}
}

