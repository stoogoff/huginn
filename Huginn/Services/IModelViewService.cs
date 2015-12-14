using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Data;
	using Huginn.Models;

	public interface IModelViewService<T,S> where T: ViewModel<S> where S: CouchData {
		/// <summary>
		/// Gets or sets the author identifier.
		/// </summary>
		/// <value>The author identifier.</value>
		int AuthorId { get; set; }

		/// <summary>
		/// Retrieve a list of all available objects.
		/// </summary>
		IList<T> All();

		/// <summary>
		/// Retrieve the object with the specified ID.
		/// </summary>
		/// <param name="id">Unique identifier for the object.</param>
		T Get(string id);

		/// <summary>
		/// Create a new object.
		/// </summary>
		/// <param name="data">The object to create.</param>
		T Create(S data);

		/// <summary>
		/// Save the supplied object with the specified ID.
		/// </summary>
		/// <param name="id">Unique identifier of the object to save.</param>
		/// <param name="data">The object to save.</param>
		T Save(string id, S data);

		/// <summary>
		/// Delete the object with the specified id and revision.
		/// </summary>
		/// <param name="id">Unique identifier of the object.</param>
		/// <param name="revision">The latest revision of the object.</param>
		bool Delete(string id, string revision);
	}
}

