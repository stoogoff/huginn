using Huginn.Managers;
using Huginn.Json;

namespace Huginn.Modules {
	public class ProfileModule: ModelModule<Huginn.Data.Profile> {
		public ProfileModule(): base("/profiles") {
			manager = new ProfileManager();

			Get["/{id}/novels"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse((manager as ProfileManager).Novels(id));
			};
		}
	}
}

