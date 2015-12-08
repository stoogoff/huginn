using Nancy.ModelBinding;
using Huginn.Managers;
using Huginn.Data;
using Huginn.Json;

namespace Huginn.Modules {
	public class BookModule: ModelModule <Huginn.Data.Book> {
		public BookModule(): base("/books") {
			manager = new BookManager();

			Get["/archive"] = parameters => ResponseHandler.GetResponse((manager as BookManager).Archive());

			Put["/{id}/sort"] = parameters => {
				var sort = this.Bind<BookSort>();

				(manager as BookManager).Sort(sort);

				return ResponseHandler.GetResponse("OK");
			};

			Get["/{id}/data"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse((manager as BookManager).Data(id));
			};

			Get["/{id}/chapters"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse((manager as BookManager).Chapters(id));
			};
			Get["/{id}/entities"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse((manager as BookManager).Entities(id));
			};
		}
	}
}

