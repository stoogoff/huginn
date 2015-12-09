using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Models;

	public interface IModelViewService<T> where T: ViewModel {
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
		T Create(T data);

		/// <summary>
		/// Save the supplied object with the specified ID.
		/// </summary>
		/// <param name="id">Unique identifier of the object to save.</param>
		/// <param name="data">The object to save.</param>
		T Save(string id, T data);

		/// <summary>
		/// Delete the object with the specified id and revision.
		/// </summary>
		/// <param name="id">Unique identifier of the object.</param>
		/// <param name="revision">The latest revision of the object.</param>
		bool Delete(string id, string revision);
	}
}

