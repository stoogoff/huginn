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
		protected IList<Entity> entities;

		public UnparsedChapterViewModel(Chapter chapter, IList<Entity> entities): base(chapter) {
			this.entities = entities;
		}

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

		[JsonProperty("word_count")]
		[XmlElement("word-count")]
		public int WordCount {
			get {
				return data.Content.ParseEntities(entities).Prettify().WordCount();
			}
		}
	}
}

