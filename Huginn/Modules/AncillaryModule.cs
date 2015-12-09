using System.Collections.Generic;

namespace Huginn.Modules {
	using Huginn.Couch;
	using Huginn.Data;
	using Huginn.Json;

	public class AncillaryModule: SecurityModule {
		public AncillaryModule() {
			// latest changes
			Get["/latest"] = parameters => GetLatest(10);

			Get["/latest/{limit:int}"] = paremeters => ResponseHandler.GetResponse(GetLatest(paremeters.limit));

			// trash
			Get["/trash"] = parameters => {
				var query = new ViewQuery {
					StartKey = ViewQuery.GetStartKey(AuthorId),
					EndKey = ViewQuery.GetEndKey(AuthorId)
				};
				var result = client.GetView<Proxy>("all", "in_trash", query);

				return result.ToList();
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

