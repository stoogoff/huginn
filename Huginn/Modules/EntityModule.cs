
namespace Huginn.Modules {
	using Huginn.Data;
	using Huginn.Models;
	using Huginn.Services;

	public class EntityModule: ModelModule<EntityViewModel, Entity> {
		public EntityModule(IEntityService service): base(service, "/entities") {
			Get["/{id}/books"] = parameters => {
				var id = parameters.id.ToString();

				return service.Books(id);
			};
		}
	}
}
	