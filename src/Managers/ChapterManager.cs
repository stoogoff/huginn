using System;
using Huginn.Data;
using Huginn.JsonModels;

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

			return model;
		}

		public override IModel Save(Chapter data) {
			var model = new ChapterJson();

			model.Chapter = SaveObject(data);

			return model;
		}

		public override IModel Save(string id, Chapter data) {
			var model = new ChapterJson();

			model.Chapter = SaveObject(data);

			return model;
		}
	}
}

