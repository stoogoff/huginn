using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Data;
	using Huginn.Models;

	public class BookService: IModelViewService<BookViewModel> {
		protected readonly DataRepository repo;

		// TODO IOC
		public BookService(int authorId) {
			repo = new DataRepository(authorId);
		}

		public IList<BookViewModel> All() {
			var books = repo.AllObjects<Book>("novels");
			var response = new List<BookViewModel>();

			// TODO loop through books and convert to BookViewModels
			foreach(var book in books) {
				response.Add(BookViewModel.Create(book));
			}

			return response;
		}

		public BookViewModel Get(string id) {
			var book = repo.GetObject<Book>(id);

			// TODO convert

			return BookViewModel.Create(book);
		}

		public BookViewModel Create(BookViewModel data) {

		}

		public BookViewModel Save(string id, BookViewModel data) {

		}

		public bool Delete(string id, string revision) {
			return repo.DeleteObject(id, revision);
		}
	}
}

