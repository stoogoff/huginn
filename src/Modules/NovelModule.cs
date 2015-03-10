using System;

namespace Huginn.Modules {
	public class NovelModule: ModelModule <Huginn.Models.Novel> {
		public NovelModule(): base("/novels") {
			Get["/view"] = parameters => {
				var client = GetClient();
				var query = new Huginn.Couch.ViewQuery{
					StartKey = "[2]",
					EndKey = "[2,{}]"
				};

				return client.GetView<Huginn.Models.Novel>("novels", "by_author", query);
			};
		}
	}
}

