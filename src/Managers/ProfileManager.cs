using System.Collections.Generic;
using Huginn.Models;
using Huginn.Couch;

namespace Huginn.Managers {
	public class ProfileManager: BaseManager<Profile> {
		public ProfileManager(): base("contributors") {

		}

		public IList<Novel> Novels(string id) {
			var query = new ViewQuery {
				StartKey = "[\"" + id + "\"]",
				EndKey = "[\"" + id + "\",{}]",
			};
			var response = client.GetView<Novel>("novels", "by_contributor", query);

			return ConvertView<Novel>(response);;
		}
	}
}

