using System.Collections.Generic;
using Huginn.Couch;
using Huginn.Data;
using Huginn.JsonModels;

namespace Huginn.Managers {
	public class EntityManager: DataManager<Entity> {
		public EntityManager(): base("entities") {

		}

		public override IModel All() {
			var model = new EntitiesJson();

			model.Entities = AllObjects<Entity>();

			return model;
		}

		public override IModel Get(string id) {
			var model = new EntityJson();

			model.Entity = GetObject<Entity>(id);

			// get novels this entity is linked to
			if(model.Entity.Novels != null && model.Entity.Novels.Count > 0) {
				var novels = AllObjects<Novel>("novels");
				var list = new List<Novel>();

				foreach(var novel in novels) {
					if(model.Entity.Novels.Contains(novel.Id))
						list.Add(novel);
				}

				model.Novels = list;
			}

			// get chapters this entity is used in
			var query = new ViewQuery {
				Key = "[" + AuthorId + ",\"" + model.Entity.Hint + "\"]"
			};
			var result = Client.GetView<ChapterSummary>(view, "by_author_usage", query);

			model.Chapters = ConvertView<ChapterSummary>(result);

			return model;
		}

		public override IModel Save(Entity data) {
			var model = new EntityJson();

			model.Entity = SaveObject(data);

			return model;
		}

		public override IModel Save(string id, Entity data) {
			var model = new EntityJson();

			model.Entity = SaveObject(data);

			return model;
		}
	}
}

