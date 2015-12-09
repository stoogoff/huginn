using System.Collections.Generic;
using Huginn.Data;

namespace Huginn.Managers {
	using Huginn.Couch;
	using Huginn.Json;
	using Huginn.Models;

	public class ProfileManager: DataManager<Profile> {
		public ProfileManager(): base("contributors") {

		}

		public override IModel All() {
			var model = new ProfilesJson();

			model.Profiles = AllObjects<Profile>();

			// TODO novel count foreach profile

			return model;
		}

		public override IModel Get(string id) {
			var model = new ProfileJson();

			model.Profile = GetObject<Profile>(id);
			model.Books = GetNovels(id);

			return model;
		}

		public override IModel Create(Profile data) {
			var model = new ProfileJson();

			model.Profile = CreateObject<Profile>(data);

			return model;
		}

		public override IModel Save(string id, Profile data) {
			var model = new ProfileJson();

			model.Profile = SaveObject(id, data);

			return model;
		}

		public BooksJson Novels(string id) {
			var model = new BooksJson();

			model.Books = GetNovels(id);

			return model;
		}

		protected IList<Book> GetNovels(string id) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(id),
				EndKey = ViewQuery.GetEndKey(id)
			};
			var response = Client.GetView<Book>("novels", "by_contributor", query);

			return response.ToList();
		}
	}
}

