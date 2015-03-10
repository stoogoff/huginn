using System;

namespace Huginn.Modules {
	public class NovelModule: ModelModule <Huginn.Models.Novel> {
		public NovelModule(): base("/novels") {}
	}
}

