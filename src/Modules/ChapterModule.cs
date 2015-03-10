using System;

namespace Huginn.Modules {
	public class ChapterModule: ModelModule <Huginn.Models.Chapter> {
		public ChapterModule(): base("/chapters") {}
	}
}
