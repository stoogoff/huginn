using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	using Huginn.Data;
	using Huginn.Extensions;

	// the idea here is to return a chapter which is ready to be edited
	// /chapters      - IList<UnparsedChapterViewModel>
	// /chapters/<id> - UnparsedChapterViewModel
	[XmlRootAttribute("chapter")]
	public class UnparsedChapterViewModel: ViewModel<Chapter> {
		public UnparsedChapterViewModel(Chapter chapter): base(chapter) { }

		// raw chapter Title
		[JsonProperty("title")]
		[XmlElement("title")]
		public string Title {
			get {
				return data.Title;
			}
		}

		// full content as its stored in Couch
		// TODO this should be a CData section in XML
		[JsonProperty("raw_content")]
		[XmlElement("raw-content")]
		public string RawContent {
			get {
				return data.Content;
			}
		}

		[JsonProperty("sort")]
		[XmlElement("sort")]
		public int Sort {
			get {
				return data.Sort;
			}
		}
	}
}

