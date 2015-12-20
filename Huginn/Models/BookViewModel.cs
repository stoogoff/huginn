using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	using Huginn.Data;
	using Huginn.Extensions;

	// /books               - IList<BookViewModel> - this *should* return all books, independent of archive state
	//												 but this may be dependent on the view code in CouchDB
	// /books/archive       - IList<BookViewModel> - only archived (Archive = true) books
	// /books/active        - IList<BookViewModel> - only active (Archive = false) books
	// /books/<id>          - BookViewModel
	// /chapters/<id>/book  - BookViewModel
	// /entities/<id>/books - IList<BookViewModel>
	// /profiles/<id>/books - IList<BookViewModel>
	[XmlRootAttribute("book")]
	public class BookViewModel: ViewModel<Book> {
		protected IList<Entity> entities;

		public BookViewModel(Book book, IList<Entity> entities): base(book) {
			this.entities = entities;
		}

		//[Cache]
		[JsonProperty("title")]
		[XmlElement("title")]
		public string Title {
			get {
				return data.Title.ParseEntities(entities).Prettify();
			}
		}

		//[Cache]
		[JsonProperty("synopsis")]
		[XmlElement("synopsis")]
		public string Synopsis {
			get {
				return data.Synopsis.ParseEntities(entities).Prettify();
			}
		}

		//[Cache]
		[JsonProperty("publisher")]
		[XmlElement("publisher")]
		public string Publisher {
			get {
				return data.Publisher.ParseEntities(entities).Prettify();
			}
		}

		[JsonProperty("include_license")]
		[XmlElement("include-license")]
		public bool IncludeLicense {
			get {
				return data.IncludeLicense ?? false;
			}
		}

		[JsonProperty("include_synopsis")]
		[XmlElement("include-synopsis")]
		public bool IncludeSynopsis {
			get {
				return data.IncludeSynopsis ?? false;
			}
		}

		[JsonProperty("archive")]
		[XmlElement("archive")]
		public bool Archive {
			get {
				return data.Archive;
			}
		}

		[JsonProperty("image")]
		[XmlElement("image")]
		public int? Image {
			get {
				return data.Image;
			}
		}

		[JsonProperty("editable")]
		[XmlElement("editable")]
		public bool Editable {
			get {
				return !(Trash || Archive);
			}
		}

		public override string ToString() {
			return string.Format("[BookViewModel: Title={0}, Synopsis={1}, Publisher={2}, IncludeLicense={3}, IncludeSynopsis={4}, Archive={5}, Image={6}, Editable={7}]", Title, Synopsis, Publisher, IncludeLicense, IncludeSynopsis, Archive, Image, Editable);
		}
	}

	/*[XmlRootAttribute("book")]
	public class BookViewModel: ViewModel {
		public BookViewModel() {
			Object = "book";
		}

		[JsonProperty("title")]
		[XmlElement("title")]
		public string Title { get; set; }

		[JsonProperty("synopsis")]
		[XmlElement("title")]
		public string Synopsis { get; set; }

		[JsonProperty("publisher")]
		[XmlElement("title")]
		public string Publisher { get; set; }

		[JsonProperty("include_license")]
		[XmlElement("include-license")]
		public bool IncludeLicense { get; set; }

		[JsonProperty("include_synopsis")]
		[XmlElement("include-synopsis")]
		public bool IncludeSynopsis { get; set; }

		[JsonProperty("archive")]
		[XmlElement("archive")]
		public bool Archive { get; set; }

		[JsonProperty("image")]
		[XmlElement("image")]
		public int? Image { get; set; }

		[JsonProperty("editable")]
		[XmlElement("editable")]
		public bool Editable {
			get {
				return !(Trash || Archive);
			}
		}

		// TODO automapper?
		// TODO JsonProperty should be a generic property converter so it handles XML too

		public static BookViewModel Create(Book book) {
			var created = new BookViewModel {
				Id = book.Id,
				Revision = book.Revision,
				Created = book.Created,
				Modified = book.Modified,
				Title = book.Title,
				Synopsis = book.Synopsis,
				Publisher = book.Publisher,
				IncludeLicense = book.IncludeLicense ?? false,
				IncludeSynopsis = book.IncludeSynopsis ?? false,
				Image = book.Image,
				Archive = book.Archive,
			};

			return created;
		}
	}

	// not sure about this?
	public class ListModel {
		public string Object {
			get {
				return "list";
			}
		}
		public IList<ViewModel> Data { get; set; }
	}*/
}

/*

/books

IList<Book>
	ID
	Revision
	Title
	Summary
	ChapterCount
	CreatedDate

{
	"id": "<id>",
	"object": "Book",
	"revision": "<id>",
	"title": "Book title",
	"summary": "Book summary",
	"word_count": 1000,
	"chapter_count": 2,
	"image": 1,
	// .... other properties
	"archive": false,
	"trash": false,
	"created": "2015-10-10",
	"modified": "2015-10-10"
}

[
	{
		"id": "<id>",
		"object": "Book",
		"revision": "<id>",
		"title": "Book title",
		"summary": "Book summary",
		"image": 1,
		"created": "2015-10-10",
		"modified": "2015-10-10"
	},
	{
		"id": "<id>",
		"object": "Book",
		"revision": "<id>",
		"title": "Book title",
		"summary": "Book summary",
		"image": 1,
		"created": "2015-10-10",
		"modified": "2015-10-10"
	}
]

/books/<id>

Book
	ID
	Title
	WordCount
	Summary
	IList<Profile> Profiles - /books/<id>/profiles
		ID
		Name
	IList<Chapter> Chapters - /books/<id>/chapters
		ID
		Title
		Summary (parsed)
		WordCount
		CreatedDate
	IList<Entity> Entities - /books/<id>/entities
		ID
		Hint
		Summary
	IList<Todo> Todos - /books/<id>/todos
		...


/chapters/<id>
	ID
	Title
	Content - parsed Markdown content
	WordCount
	CreatedDate - ? should this be modified?
	IList<Todo> Todos - /chapters/<id>/todos

*/












/*

The following link rel values could possibly be used for the Link HTTP header:

alternate - json/xml alternative version
author - URL of the profile(s) related to this book
license - URL of the license document for the book
next - URL of next chapter
prev - URL of previous chapter

Format:

Link: <url.json>; rel="next"; title="Chapter Title"

Or:

Link: <nextChapter.id>; rel="next"; title="Chapter Title", <previousChapter.id>; rel="prev"; title="Chapter Title"

Check the date serializer is actually required - it should be ISO8601 which Nancy produces

Have a look at these for replacing the serialiser:
- http://beletsky.net/2012/08/camelcase-json-formatting-for-nancyfx.html
- https://groups.google.com/forum/#!topic/nancy-web-framework/aSQ0PqQWZCk

How to do filtering?
	?<property>=<required value> - all objects where property == required value
	?<property>=-<required value> - all objects where property != required value
What about sorting?
	Everything should have a default sort based on Title / Name / Hint
	/books/<id>/chapters will sort on their sort order within the book
	Consuming applications should handle sorting themselves
How to tie in redis for property caching?
Convert the Managers to a Service / Repository model

*/

/*

Maybe CouchDB views should be converted so they're generic they way they are for Odin

JS...

function(doc) {
	if(!doc.trash)
		emit([doc.author, doc.doc_type], doc);
}



all/views/by_author_date
all/views/in_trash
articles/view/by_author - emit(doc.author, doc); (no sort)
articles/view/by_novel
contributors/view/by_author - emit([doc.author, doc.name], doc); (sort by doc.name)
entities/views/by_author - emit([doc.author, doc.hint], doc); (sort by doc.hint)
entities/views/by_author_usage
novels/views/archived_by_author - emit([doc.author, doc.title], doc); (sort by title, also filters so only archived are returned)
novels/views/by_author - emit([doc.author, doc.title], doc); (sort by title, also filters so only unarchived are returned)
novels/views/by_contributor
novels/views/document_count
stats/views/by_day
stats/views/by_month
stats/views/last_write
stats/views/objects_by_author




Bulk update of couch documents:

https://gesellix.net/bulk-update-of-couchdb-documents/

*/