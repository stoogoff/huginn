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

			// get the next sibling for this chapter
			var siblings = novelManager.GetChaptersForNovel(model.Novel.Id);
			var next = false;

			foreach(var sibling in siblings) {
				if(next) {
					model.Chapter.NextSibling = sibling.Id;
					break;
				}

				next = sibling.Id == id;
			}

			return model;
		}

		public override IModel Create(Chapter data) {
			var novelManager = new NovelManager();
			var chapters = novelManager.GetChaptersForNovel(data.Novel);

			// set the correct sort order for the chapter
			if(chapters.Count == 0) {
				data.Sort = 1;
			}
			else {
				// can't use chapters.Length here as it wouldn't take into account deleted chapters
				var last = chapters.Reverse().Take(1).Single();

				data.Sort = last.Sort + 1;
			}

			var model = new ChapterJson();

			model.Chapter = CreateObject<Chapter>(data);

			// create a new stats object and save it
			// this needs to be done after the chapter is saved because the new Chapter won't have an ID
			var stats = Stats.CreateFromChapter(model.Chapter);

			CreateObject<Stats>(stats);

			return model;
		}

		public override IModel Save(string id, Chapter data) {
			// get the current Chapter and store its word count
			var current = GetObject<Chapter>(id);

			// create a new stats object and save it
			var stats = Stats.CreateFromChapter(data, data.WordCount - current.WordCount);

			CreateObject<Stats>(stats);

			var model = new ChapterJson();

			model.Chapter = SaveObject<Chapter>(id, data);

			return model;
		}
	}
}

