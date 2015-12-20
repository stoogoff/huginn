using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Couch;
	using Huginn.Data;
	using Huginn.Models;

	public interface IBookService: IModelViewService<BookViewModel, Book> {
		IList<BookViewModel> Archived();
		IList<ParsedChapterViewModel> Chapters(string id);
		ParsedChapterViewModel Chapter(string bookId, string chapterId);
		IList<EntityViewModel> Entities(string id);
		IList<ProfileViewModel> Profiles(string id);
	}

	// consider converting this to a single service which can handle getting everything as it's all connected up anyway

	public class BookService: BaseService, IBookService {
		public BookService(IDataRepository repo): base(repo) { }

		#region implement IModelViewService
		public IList<BookViewModel> All() {
			var books = Repository.AllObjects<Book>("novels");
			var response = new List<BookViewModel>();

			// loop through books and convert to view model
			foreach(var book in books) {
				response.Add(new BookViewModel(book, GetEntitiesForBook(book.Id)));
			}

			return response;
		}

		public BookViewModel Get(string id) {
			var book = Repository.GetObject<Book>(id);

			return new BookViewModel(book, GetEntitiesForBook(book.Id));
		}

		public BookViewModel Create(Book data) {
			var book = Repository.CreateObject(data);

			return new BookViewModel(book, GetEntitiesForBook(book.Id));
		}

		public BookViewModel Save(string id, Book data) {
			var book = Repository.SaveObject(id, data);

			return new BookViewModel(book, GetEntitiesForBook(book.Id));
		}
		#endregion

		#region implement IBookService
		public IList<BookViewModel> Archived() {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};
			var books = Repository.View<Book>("novels", "archived_by_author", query);
			var response = new List<BookViewModel>();

			foreach(var book in books) {
				response.Add(new BookViewModel(book, GetEntitiesForBook(book.Id)));
			}

			return response;
		}

		public IList<ParsedChapterViewModel> Chapters(string id) {
			var chapters = GetChaptersForBook(id);
			var entities = GetEntitiesForBook(id);
			var response = new List<ParsedChapterViewModel>();

			// loop through books and convert to view model
			foreach(var chapter in chapters) {
				response.Add(new ParsedChapterViewModel(chapter, entities, chapters));
			}

			return response;
		}

		public ParsedChapterViewModel Chapter(string bookId, string chapterId) {
			var chapter = Repository.GetObject<Chapter>(chapterId);

			// TODO proper exceptions
			if(chapter.Book != bookId) {
				throw new System.Exception("Book doesn't belong to chapter");
			}

			return new ParsedChapterViewModel(chapter, GetEntitiesForBook(bookId), GetChaptersForBook(bookId));
		}

		public IList<EntityViewModel> Entities(string id) {
			var entities = GetEntitiesForBook(id);
			var response = new List<EntityViewModel>();

			// loop through books and convert to view model
			foreach(var entity in entities) {
				response.Add(new EntityViewModel(entity));
			}

			return response;
		}

		public IList<ProfileViewModel> Profiles(string id) {
			var book = Repository.GetObject<Book>(id);
			var profiles = Repository.AllObjects<Profile>("contributors");
			var response = new List<ProfileViewModel>();

			// loop through books and convert to view model if the profile belongs to the book
			foreach(var profile in profiles) {
				if(book.Contributors.Contains(profile.Id)) {
					response.Add(new ProfileViewModel(profile));
				}
			}

			return response;
		}
		#endregion

		/*
		 public void Sort(BookSort sort) {
			var raw = GetChaptersForNovel(sort.Id);
			var chapters = new Dictionary<string, Chapter>();

			foreach(var chapter in raw) {
				chapters.Add(chapter.Id, chapter);
			}

			int index = 0;

			foreach(var id in sort.Chapters) {
				chapters[id].Sort = ++index;

				SaveObject<Chapter>(id, chapters[id]);
			}
		}*/
	}
}

