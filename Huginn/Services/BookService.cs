using System;
using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Data;
	using Huginn.Models;

	public interface IBookService: IModelViewService<BookViewModel,Book> {

	}

	public class BookService: BaseService, IBookService {
		// TODO IOC
		public BookService(IDataRepository repo): base(repo) { }

		#region implement IModelViewService
		public IList<BookViewModel> All() {
			var books = Repository.AllObjects<Book>("novels");
			var response = new List<BookViewModel>();

			// loop through books and convert to BookViewModels
			foreach(var book in books) {
				response.Add(new BookViewModel(book, GetEntities(book)));

				Console.WriteLine("-- Getting book ({0}), {1}", book.Id, book.Title);
				Console.WriteLine("-- {0}", book.Created);
			}

			return response;
		}

		public BookViewModel Get(string id) {
			var book = Repository.GetObject<Book>(id);

			// TODO convert

			return new BookViewModel(book, GetEntities(book));
		}

		public BookViewModel Create(Book data) {
			var book = Repository.CreateObject(data);

			return new BookViewModel(book, GetEntities(book));
		}

		public BookViewModel Save(string id, Book data) {
			var book = Repository.SaveObject(id, data);

			return new BookViewModel(book, GetEntities(book));
		}

		public bool Delete(string id, string revision) {
			return Repository.DeleteObject(id, revision);
		}
		#endregion

		// TODO - cache entities
		protected IList<Entity> GetEntities(Book book) {
			var entities = Repository.AllObjects<Entity>("entities");
			var response = new List<Entity>();

			foreach(var entity in entities) {
				if(entity.Books != null && entity.Books.Contains(book.Id))
					response.Add(entity);
			}

			return response;
		}
	}
}

