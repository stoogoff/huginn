
namespace Huginn.Modules {
	using Huginn.Data;
	using Huginn.Models;
	using Huginn.Services;

	public class ChapterModule: ModelModule<UnparsedChapterViewModel, Chapter> {
		public ChapterModule(IChapterService service): base(service, "/chapters") {
			Get["/{id}/book"] = parameters => {
				var id = parameters.id.ToString();

				return service.Book(id);
			};

			Get["/{id}/entities"] = parameters => {
				var id = parameters.id.ToString();

				return service.Entities(id);
			};
		}
	}
}
