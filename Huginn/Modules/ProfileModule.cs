
namespace Huginn.Modules {
	using Huginn.Services;
	using Huginn.Models;
	using Huginn.Data;

	public class ProfileModule: ModelModule<ProfileViewModel, Profile> {
		public ProfileModule(IProfileService service): base(service, "/profiles") {
			Get["/{id}/books"] = parameters => {
				var id = parameters.id.ToString();

				return service.Books(id);
			};
		}
	}
}

