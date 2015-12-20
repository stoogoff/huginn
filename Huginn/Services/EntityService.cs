using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Data;
	using Huginn.Models;

	public interface IEntityService: IModelViewService<EntityViewModel, Entity> {
		IList<BookViewModel> Books(string id);
	}

	public class EntityService: BaseService, IEntityService {
		public EntityService(IDataRepository repo): base(repo) { }

		#region implement IModelViewService
		public IList<EntityViewModel> All() {
			var entities = Repository.AllObjects<Entity>("entities");
			var response = new List<EntityViewModel>();

			// loop through books and convert to view model
			foreach(var entity in entities) {
				response.Add(new EntityViewModel(entity));
			}

			return response;
		}

		public EntityViewModel Get(string id) {
			var entity = Repository.GetObject<Entity>(id);

			return new EntityViewModel(entity);
		}

		public EntityViewModel Create(Entity data) {
			var entity = Repository.CreateObject(data);

			return new EntityViewModel(entity);
		}

		public EntityViewModel Save(string id, Entity data) {
			var entity = Repository.SaveObject(id, data);

			return new EntityViewModel(entity);
		}
		#endregion

		#region implement IEntityService
		public IList<BookViewModel> Books(string id) {
			var entity = Repository.GetObject<Entity>(id);
			var response = new List<BookViewModel>();

			if(entity.Books.Count > 0) {
				foreach(var bookId in entity.Books) {
					var book = Repository.GetObject<Book>(bookId);

					response.Add(new BookViewModel(book, GetEntitiesForBook(bookId)));
				}
			}

			return response;
		}
		#endregion
	}
}
