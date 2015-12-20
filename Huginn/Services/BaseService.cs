using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Couch;
	using Huginn.Data;
	using Huginn.Exceptions;

	public abstract class BaseService {
		private int authorId;

		protected BaseService(IDataRepository repo) {
			Repository = repo;
		}

		public IDataRepository Repository { get; private set; }

		/// <summary>
		/// Gets or sets the author id.
		/// </summary>
		/// <value>The author identifier.</value>
		public int AuthorId {
			get {
				return authorId;
			}
			set {
				authorId = value;

				if(Repository != null) {
					Repository.AuthorId = value;
				}
			}
		}

		/// <summary>
		/// Delete the specified object by its id and revision.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="revision">Revision.</param>
		public bool Delete(string id, string revision) {
			return Repository.DeleteObject(id, revision);
		}

		/// <summary>
		/// Gets an object from the repository and handles null checks and authorisation checks.
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="id">Identifier.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected T GetObject<T>(string id) where T: CouchData {
			var obj = Repository.GetObject<T>(id);

			if(obj == null) {
				throw ServiceException.NotFound(id);
			}

			if(obj.Author != AuthorId) {
				throw ServiceException.Forbidden(obj.Id);
			}

			return obj;
		}

		/// <summary>
		/// Gets the entities for the supplied book id.
		/// </summary>
		/// <returns>The entities for book.</returns>
		/// <param name="id">Identifier.</param>
		protected IList<Entity> GetEntitiesForBook(string id) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(id),
				EndKey = ViewQuery.GetEndKey(id),
			};

			return Repository.View<Entity>("entities", "by_novel", query);
		}

		/// <summary>
		/// Gets the chapters for the supplied book.
		/// </summary>
		/// <returns>The chapters for book.</returns>
		/// <param name="id">Identifier.</param>
		protected IList<Chapter> GetChaptersForBook(string id) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(id),
				EndKey = ViewQuery.GetEndKey(id)
			};

			return Repository.View<Chapter>("articles", "by_novel", query);
		}
	}
}

