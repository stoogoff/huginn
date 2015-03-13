using Huginn.Managers;

namespace Huginn.Modules {
	public class NovelModule: ModelModule <Huginn.Data.Novel> {
		public NovelModule(): base("/novels") {
			manager = new NovelManager();

			Get["/archive"] = parameters => GetResponse((manager as NovelManager).Archive());

			Get["/{id}/data"] = parameters => {
				var id = parameters.id.ToString();

				return GetResponse((manager as NovelManager).Data(id));
			};

			Get["/{id}/chapters"] = parameters => {
				var id = parameters.id.ToString();

				return GetResponse((manager as NovelManager).Chapters(id));
			};
			Get["/{id}/entities"] = parameters => {
				var id = parameters.id.ToString();

				return GetResponse((manager as NovelManager).Entities(id));
			};
		}
	}
}

