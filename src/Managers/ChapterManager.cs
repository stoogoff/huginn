using System.Linq;
using Huginn.Data;
using Huginn.Json;

namespace Huginn.Managers {
	public class ChapterManager: DataManager<Chapter> {
		public ChapterManager(): base("articles") {
		}

		public override IModel All() {
			var model = new ChaptersJson();

			model.Chapters = AllObjects<Chapter>();

			return model;
		}

		public override IModel Get(string id) {
			var model = new ChapterJson();

			model.Chapter = GetObject<Chapter>(id);
			model.Novel = GetObject<Novel>(model.Chapter.Novel);

			// get the entities which can be applied to this chapter
			var novelManager = new NovelManager();

			novelManager.AuthorId = AuthorId;

			model.Entities = novelManager.GetEntitiesForNovel(model.Novel.Id);

			return model;
		}

		public override IModel Create(Chapter data) {
			var novelManager = new NovelManager();
			var chapters = novelManager.GetChaptersForNovel(data.Novel);

			if(chapters.Count == 0) {
				data.Sort = 1;
			}
			else {
				// can't use chapters.Length here as it wouldn't take into account deleted chapters
				var last = chapters.Reverse().Take(1).Single();

				data.Sort = last.Sort + 1;
			}

			var model = new ChapterJson();

			model.Chapter = CreateObject(data);

			return model;
		}

		public override IModel Save(string id, Chapter data) {
			var model = new ChapterJson();

			model.Chapter = SaveObject(id, data);

			return model;
		}
	}
}

