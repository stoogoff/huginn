using System.Collections.Generic;

namespace Huginn.Managers {
	using Huginn.Couch;
	using Huginn.Data;
	using Huginn.Json;
	using Huginn.Models;

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
			if(model.Entity.Books != null && model.Entity.Books.Count > 0) {
				var novels = AllObjects<Book>("novels");
				var list = new List<Book>();

				foreach(var novel in novels) {
					if(model.Entity.Books.Contains(novel.Id))
						list.Add(novel);
				}

				model.Books = list;
			}

			// get chapters this entity is used in
			var query = new ViewQuery {
				Key = "[" + AuthorId + ",\"" + model.Entity.Hint + "\"]"
			};
			var result = Client.GetView<ChapterSummary>(view, "by_author_usage", query);

			model.Chapters = result.ToList();

			return model;
		}

		public override IModel Create(Entity data) {
			var model = new EntityJson();

			model.Entity = CreateObject(data);

			return model;
		}

		public override IModel Save(string id, Entity data) {
			var model = new EntityJson();

			model.Entity = SaveObject(id, data);

			return model;
		}
	}
}

