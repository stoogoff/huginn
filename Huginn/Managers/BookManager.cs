using System.Collections.Generic;

namespace Huginn.Managers {
	using Huginn.Data;
	using Huginn.Couch;
	using Huginn.Json;
	using Huginn.Models;

	public class BookManager: DataManager<Book> {
		public BookManager(): base("novels") {}

		public override IModel All() {
			// TODO chapter count

			var model = new BooksJson();

			model.Books = AllObjects<Book>();

			var query = new ViewQuery {
				Group = true,
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};
			var count = Client.GetView<int>(view, "document_count", query);
			var dict = new Dictionary<string, int>();

			foreach(var row in count.Rows) {
				// row.Key[0] is the author id, row.Key[1] is the novel id
				dict.Add(row.Key[1].ToString(), row.Value);
			}

			/*foreach(var novel in model.Books) {
				if(dict.ContainsKey(novel.Id)) {
					novel.ChapterCount = dict[novel.Id];
				}
			}*/

			return model;
		}

		public override IModel Get(string id) {
			var model = new BookJson();

			model.Book = GetObject<Book>(id);

			return model;
		}

		/*public BookViewModel Get2(string id) {
			var book = GetObject<Book>(id);

			return BookViewModel.Create(book);
		}*/

		public override IModel Create(Book data) {
			var model = new BookJson();

			model.Book = CreateObject(data);

			return model;
		}

		public override IModel Save(string id, Book data) {
			var model = new BookJson();

			model.Book = SaveObject(id, data);

			return model;
		}

		public override bool Delete(string id, string revision) {
			var result = base.Delete(id, revision);

			// delete all chapters relating to this novel
			var chapters = GetChaptersForNovel(id);

			foreach(var chapter in chapters) {
				base.Delete(chapter.Id, chapter.Revision);
			}

			return result;
		}

		public ChaptersJson Chapters(string id) {
			var model = new ChaptersJson();

			model.Chapters = GetChaptersForNovel(id);

			return model;
		}

		public BookJson Data(string id) {
			var model = new BookJson();

			model.Book = GetObject<Book>(id);
			model.Chapters = GetChaptersForNovel(id);
			model.Entities = GetEntitiesForNovel(id);
			model.Profiles = GetProfilesForNovel(model.Book.Contributors);

			return model;
		}

		public BooksJson Archive() {
			var model = new BooksJson();
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};
			var response = Client.GetView<Book>(view, "archived_by_author", query);

			model.Books = response.ToList();

			return model;
		}

		public void Sort(BookSort sort) {
			var raw = GetChaptersForNovel(sort.Id);
			var chapters = new Dictionary<string, Chapter>();

			foreach(var chapter in raw) {
				chapters.Add(chapter.Id, chapter);
			}

			int index = 0;

			foreach(var id in sort.Chapters) {
				chapters[id].Sort = ++index;

				SaveObject<Chapter>(id, chapters[id]);
			}
		}

		public EntitiesJson Entities(string id) {
			var model = new EntitiesJson();

			model.Entities = GetEntitiesForNovel(id);

			return model;
		}

		public IList<Chapter> GetChaptersForNovel(string id) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(id),
				EndKey = ViewQuery.GetEndKey(id)
			};
			var response = Client.GetView<Chapter>("articles", "by_novel", query);
			var chapters = response.ToList();
			/*var entities = Entities(id);

			foreach(var chapter in chapters) {
				chapter.Summarise().ConvertEntities(entities.Entities);
			}*/

			return chapters;
		}

		public IList<Entity> GetEntitiesForNovel(string id) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};
			var response = Client.GetView<Entity>("entities", "by_author", query);
			var entities = response.ToList();
			var list = new List<Entity>();

			foreach(var entity in entities) {
				if(entity.Books != null && entity.Books.Contains(id))
					list.Add(entity);
			}

			return list;
		}

		public IList<Profile> GetProfilesForNovel(IList<string> ids) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(AuthorId),
				EndKey = ViewQuery.GetEndKey(AuthorId)
			};
			var response = Client.GetView<Profile>("contributors", "by_author", query);
			var profiles = response.ToList();
			var list = new List<Profile>();

			foreach(var profile in profiles) {
				if(ids.Contains(profile.Id))
					list.Add(profile);
			}

			return list;
		}
	}
}

