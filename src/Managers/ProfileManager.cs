using System.Collections.Generic;
using Huginn.Data;
using Huginn.Couch;
using Huginn.JsonModels;

namespace Huginn.Managers {
	public class ProfileManager: DataManager<Profile> {
		public ProfileManager(): base("contributors") {

		}

		public override IModel All() {
			var model = new ProfilesJson();

			model.Profiles = AllObjects<Profile>();

			return model;
		}

		public override IModel Get(string id) {
			var model = new ProfileJson();

			model.Profile = GetObject<Profile>(id);
			model.Novels = GetNovels(id);

			return model;
		}

		public override IModel Save(Profile data) {
			var model = new ProfileJson();

			model.Profile = SaveObject<Profile>(data);

			return model;
		}

		public override IModel Save(string id, Profile data) {
			var model = new ProfileJson();

			model.Profile = SaveObject<Profile>(data);

			return model;
		}

		public NovelsJson Novels(string id) {
			var model = new NovelsJson();

			model.Novels = GetNovels(id);

			return model;
		}

		protected IList<Novel> GetNovels(string id) {
			var query = new ViewQuery {
				StartKey = "[\"" + id + "\"]",
				EndKey = "[\"" + id + "\",{}]",
			};
			var response = Client.GetView<Novel>("novels", "by_contributor", query);

			return ConvertView<Novel>(response);
		}
	}
}

