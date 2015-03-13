using Nancy.ModelBinding;
using Huginn.Managers;
using Huginn.Data;
using Huginn.Json;

namespace Huginn.Modules {
	public class NovelModule: ModelModule <Huginn.Data.Novel> {
		public NovelModule(): base("/novels") {
			manager = new NovelManager();

			Get["/archive"] = parameters => ResponseHandler.GetResponse((manager as NovelManager).Archive());

			Put["/{id}/sort"] = parameters => {
				var sort = this.Bind<NovelSort>();

				(manager as NovelManager).Sort(sort);

				return ResponseHandler.GetResponse("OK");
			};

			Get["/{id}/data"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse((manager as NovelManager).Data(id));
			};

			Get["/{id}/chapters"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse((manager as NovelManager).Chapters(id));
			};
			Get["/{id}/entities"] = parameters => {
				var id = parameters.id.ToString();

				return ResponseHandler.GetResponse((manager as NovelManager).Entities(id));
			};
		}
	}
}

