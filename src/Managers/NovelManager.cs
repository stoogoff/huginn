using System.Collections.Generic;
using Huginn.Models;
using Huginn.Couch;

namespace Huginn.Managers {
	public class NovelManager: BaseManager<Novel> {
		public NovelManager(): base("novels") {
		}

		public override IList<Novel> All() {
			// TODO chapter count
			// TODO word count

			var novels = base.All();



			return novels;
		}

		public IList<Chapter> Chapters(string id) {
			var query = new ViewQuery {
				StartKey = "[\"" + id + "\"]",
				EndKey = "[\"" + id + "\",{}]",
			};
			var response = client.GetView<Chapter>("articles", "by_novel", query);
			var chapters = ConvertView<Chapter>(response);
			var entities = Entities(id);

			foreach(var chapter in chapters) {
				chapter.Count().Summarise().ConvertEntities(entities);
			}

			return chapters;
		}

		public IList<Entity> Entities(string id) {
			var query = new ViewQuery {
				StartKey = "[" + AuthorId + "]",
				EndKey = "[" + AuthorId + ",{}]",
			};
			var response = client.GetView<Entity>("entities", "by_author", query);
			var entities = ConvertView<Entity>(response);
			var list = new List<Entity>();

			foreach(var entity in entities) {
				if(entity.Novels != null && entity.Novels.Contains(id))
					list.Add(entity);
			}

			return list;
		}
	}
}

