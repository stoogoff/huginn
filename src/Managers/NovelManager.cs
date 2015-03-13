﻿using System.Collections.Generic;
using Huginn.Data;
using Huginn.Couch;
using Huginn.JsonModels;

namespace Huginn.Managers {
	public class NovelManager: DataManager<Novel> {
		public NovelManager(): base("novels") {}

		public override IModel All() {
			// TODO chapter count

			var model = new NovelsJson();

			model.Novels = AllObjects<Novel>();

			return model;
		}

		public override IModel Get(string id) {
			var model = new NovelJson();

			model.Novel = GetObject<Novel>(id);

			return model;
		}

		public override IModel Create(Novel data) {
			var model = new NovelJson();

			model.Novel = CreateObject(data);

			return model;
		}

		public override IModel Save(string id, Novel data) {
			var model = new NovelJson();

			model.Novel = SaveObject(id, data);

			return model;
		}

		public ChaptersJson Chapters(string id) {
			var model = new ChaptersJson();

			model.Chapters = GetChaptersForNovel(id);

			return model;
		}

		public NovelJson Data(string id) {
			var model = new NovelJson();

			model.Novel = GetObject<Novel>(id);
			model.Chapters = GetChaptersForNovel(id);
			model.Entities = GetEntitiesForNovel(id);
			model.Profiles = GetProfilesForNovel(model.Novel.Contributors);

			return model;
		}

		public NovelsJson Archive() {
			var model = new NovelsJson();
			var query = new ViewQuery {
				StartKey = "[" + AuthorId + "]",
				EndKey = "[" + AuthorId + ",{}]"
			};
			var response = Client.GetView<Novel>(view, "archived_by_author", query);

			model.Novels = ConvertView<Novel>(response);

			return model;
		}

		public EntitiesJson Entities(string id) {
			var model = new EntitiesJson();

			model.Entities = GetEntitiesForNovel(id);

			return model;
		}

		public IList<Chapter> GetChaptersForNovel(string id) {
			var query = new ViewQuery {
				StartKey = "[\"" + id + "\"]",
				EndKey = "[\"" + id + "\",{}]",
			};
			var response = Client.GetView<Chapter>("articles", "by_novel", query);
			var chapters = ConvertView<Chapter>(response);
			/*var entities = Entities(id);

			foreach(var chapter in chapters) {
				chapter.Summarise().ConvertEntities(entities.Entities);
			}*/

			return chapters;
		}

		public IList<Entity> GetEntitiesForNovel(string id) {
			var query = new ViewQuery {
				StartKey = "[" + AuthorId + "]",
				EndKey = "[" + AuthorId + ",{}]",
			};
			var response = Client.GetView<Entity>("entities", "by_author", query);
			var entities = ConvertView<Entity>(response);
			var list = new List<Entity>();

			foreach(var entity in entities) {
				if(entity.Novels != null && entity.Novels.Contains(id))
					list.Add(entity);
			}

			return list;
		}

		public IList<Profile> GetProfilesForNovel(IList<string> ids) {
			var query = new ViewQuery {
				StartKey = "[" + AuthorId + "]",
				EndKey = "[" + AuthorId + ",{}]",
			};
			var response = Client.GetView<Profile>("contributors", "by_author", query);
			var profiles = ConvertView<Profile>(response);
			var list = new List<Profile>();

			foreach(var profile in profiles) {
				if(ids.Contains(profile.Id))
					list.Add(profile);
			}

			return list;
		}
	}
}

