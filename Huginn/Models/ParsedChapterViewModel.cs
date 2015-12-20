using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	using Huginn.Data;
	using Huginn.Extensions;

	// the idea here is to return a chapter which is ready for conversion to EPUB (via a Markdown converter)
	// /books/<id>/chapters      - IList<ParsedChapterViewModel>
	// /books/<id>/chapters/<id> - ParsedChapterViewModel
	public class ParsedChapterViewModel: ViewModel<Chapter> {
		protected IList<Entity> entities;
		protected IList<Chapter> chapters;

		public ParsedChapterViewModel(Chapter chapter, IList<Entity> entities, IList<Chapter> chapters): base(chapter) {
			this.entities = entities;
			this.chapters = chapters;
		}

		// returns "Unnamed Chapter" if not set
		// full Title which has gone through the following process:
		//	- entities replaced
		//	- typography converted
		//[Cache]
		[JsonProperty("title")]
		[XmlElement("title")]
		public string Title {
			get {
				return data.Title.ParseEntities(entities).Prettify();
			}
		}

		// full Content which has gone through the following process:
		//	- entities replaced
		//	- todos removed
		//	- typography converted
		// TODO this should probably be a CData section
		//[Cache]
		[JsonProperty("content")]
		[XmlElement("content")]
		public string Content {
			get {
				return data.Content.ParseEntities(entities).Prettify();
			}
		}

		[JsonProperty("sort")]
		[XmlElement("sort")]
		public int Sort {
			get {
				return data.Sort;
			}
		}

		//[Cache]
		[JsonProperty("word_count")]
		[XmlElement("word-count")]
		public int WordCount {
			get {
				return Content.WordCount();
			}
		}

		// handle via Link header
		// This could also be achieved using a Link header
		// see http://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api#pagination
		// ID of next chapter or null (or Proxy instance?)
		//[LinkHeader(Rel="next")]
		//[JsonIgnore]
		[JsonProperty("next_chapter")]
		[XmlIgnore]
		public string NextChapter {
			get {
				bool getNext = false;

				foreach(var chapter in chapters) {
					if(getNext) {
						return chapter.Id;
					}

					if(chapter.Id == Id) {
						getNext = true;
					}
				}

				return null;
			}
		}

		// ID of previous chapter or null (or Proxy instance?)
		//[LinkHeader(Rel="previous")]
		//[JsonIgnore]
		[JsonProperty("previous_chapter")]
		[XmlIgnore]
		public string PreviousChapter {
			get {
				string prevChapter = null;

				foreach(var chapter in chapters) {
					if(chapter.Id == Id) {
						return prevChapter;
					}

					prevChapter = chapter.Id;
				}

				return null;
			}
		}
	}
}

