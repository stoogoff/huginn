
namespace Huginn.Modules {
	using Huginn.Data;
	using Huginn.Managers;

	public class ChapterModule: ModelModule<Chapter> {
		public ChapterModule(): base("/chapters") {
			manager = new ChapterManager();
		}
	}
}
