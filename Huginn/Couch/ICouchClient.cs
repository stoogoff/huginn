using System;

namespace Huginn.Couch {
	public interface ICouchClient {
		string NextId();

		#region Single object crud methods
		T GetDocument<T>(string id);
		CouchResponse Save(object model);
		CouchResponse Save(string id, object model);
		CouchResponse Delete(string id, string revision);
		#endregion

		#region Views
		ViewResult<T> GetView<T>(string designDoc, string view);
		ViewResult<T> GetView<T>(string designDoc, string view, ViewQuery query);
		ViewResult GetView(string designDoc, string view);
		ViewResult GetView(string designDoc, string view, ViewQuery query);
		#endregion
	}
}

