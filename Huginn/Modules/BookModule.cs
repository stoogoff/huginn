using Nancy.ModelBinding;

namespace Huginn.Modules {
	using Huginn.Services;
	using Huginn.Data;
	using Huginn.Models;

	public class BookModule: ModelModule<BookViewModel, Book> {
		public BookModule(IBookService service): base(service, "/books") {
			Get["/archived"] = parameters => service.Archived();

			/*Put["/{id}/sort"] = parameters => {
				var sort = this.Bind<BookSort>();

				(manager as BookManager).Sort(sort);

				return "OK";
			};*/

			Get["/{id}/chapters"] = parameters => {
				var id = parameters.id.ToString();

				return service.Chapters(id);
			};
			Get["/{book}/chapters/{chapter}"] = parameters => {
				var book = parameters.book.ToString();
				var chapter = parameters.chapter.ToString();

				return service.Chapter(book, chapter);
			};
			Get["/{id}/entities"] = parameters => {
				var id = parameters.id.ToString();

				return service.Entities(id);
			};
			Get["/{id}/profiles"] = parameters => {
				var id = parameters.id.ToString();

				return service.Profiles(id);
			};
		}
	}
}

