using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Data;

	public interface IDataRepository {
		int AuthorId { get; set; }

		IList<T> AllObjects<T>(string view);

		T GetObject<T>(string id);

		// save a new object
		T CreateObject<T>(T model) where T: CouchData;

		// save an existing object
		T SaveObject<T>(string id, T model) where T: CouchData;

		// delete an existing object
		bool DeleteObject(string id, string revision);
	}
}

