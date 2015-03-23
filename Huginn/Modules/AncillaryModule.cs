using System.Collections.Generic;
using Nancy;
using Huginn.Couch;
using Huginn.Data;
using Huginn.Json;

namespace Huginn.Modules {
	public class AncillaryModule: SecurityModule {
		public AncillaryModule() {
			// latest changes
			Get["/latest"] = parameters => ResponseHandler.GetResponse(GetLatest(10));

			Get["/latest/{limit:int}"] = paremeters => ResponseHandler.GetResponse(GetLatest(paremeters.limit));

			// trash
			Get["/trash"] = parameters => {
				var query = new ViewQuery {
					StartKey = ViewQuery.GetStartKey(AuthorId),
					EndKey = ViewQuery.GetEndKey(AuthorId)
				};
				var result = client.GetView<Proxy>("all", "in_trash", query);

				return ResponseHandler.GetResponse(result.ToList());
			};
		}

		protected IList<Proxy> GetLatest(int limit) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId),
				Limit = limit
			};
			var result = client.GetView<Proxy>("all", "by_author_date", query);

			return result.ToList();
		}
	}
}

