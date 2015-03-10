using System;
using Huginn.Managers;

namespace Huginn.Modules {
	public class NovelModule: ModelModule <Huginn.Models.Novel> {
		public NovelModule(): base("/novels") {
			manager = new NovelManager();

			Get["/{id}/chapters"] = parameters => {
				var id = parameters.id.ToString();

				return (manager as NovelManager).Chapters(id);
			};
			Get["/{id}/entities"] = parameters => {
				var id = parameters.id.ToString();

				return (manager as NovelManager).Entities(id);
			};
		}
	}
}

