using Huginn.Managers;
using Huginn.Exceptions;

namespace Huginn.Modules {
	public class ProfileModule: ModelModule<Huginn.Data.Profile> {
		public ProfileModule(): base("/profiles") {
			manager = new ProfileManager();

			Get["/{id}/novels"] = parameters => {
				try {
					var id = parameters.id.ToString();

					return GetResponse((manager as ProfileManager).Novels(id));
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};
		}
	}
}

