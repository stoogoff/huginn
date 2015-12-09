﻿using Nancy.ModelBinding;

namespace Huginn.Modules {
	using Huginn.Managers;
	using Huginn.Data;
	using Huginn.Json;

	public class BookModule: ModelModule <Huginn.Data.Book> {
		public BookModule(): base("/books") {
			manager = new BookManager();

			/*Get["/test/{id}"] = parameters => {
				var id = parameters.id.ToString();

				return (manager as BookManager).Get2(id);
			};*/

			Get["/archive"] = parameters => (manager as BookManager).Archive();

			Put["/{id}/sort"] = parameters => {
				var sort = this.Bind<BookSort>();

				(manager as BookManager).Sort(sort);

				return "OK";
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

