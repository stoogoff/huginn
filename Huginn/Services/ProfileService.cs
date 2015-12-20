using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Couch;
	using Huginn.Data;
	using Huginn.Models;

	public interface IProfileService: IModelViewService<ProfileViewModel, Profile> {
		IList<BookViewModel> Books(string id);
	}

	public class ProfileService: BaseService, IProfileService {
		public ProfileService(IDataRepository repo): base(repo) { }

		#region implement IModelViewService
		public IList<ProfileViewModel> All() {
			var profiles = Repository.AllObjects<Profile>("contributors");
			var response = new List<ProfileViewModel>();

			// loop through books and convert to view model
			foreach(var profile in profiles) {
				response.Add(new ProfileViewModel(profile));
			}

			return response;
		}

		public ProfileViewModel Get(string id) {
			var profile = Repository.GetObject<Profile>(id);

			return new ProfileViewModel(profile);
		}

		public ProfileViewModel Create(Profile data) {
			var profile = Repository.CreateObject(data);

			return new ProfileViewModel(profile);
		}

		public ProfileViewModel Save(string id, Profile data) {
			var profile = Repository.SaveObject(id, data);

			return new ProfileViewModel(profile);
		}
		#endregion

		#region implement IProfileService
		public IList<BookViewModel> Books(string id) {
			var query = new ViewQuery {
				StartKey = ViewQuery.GetStartKey(id),
				EndKey = ViewQuery.GetEndKey(id),
			};

			var books = Repository.View<Book>("novels", "by_contributor", query);
			var response = new List<BookViewModel>();

			foreach(var book in books) {
				response.Add(new BookViewModel(book, GetEntitiesForBook(book.Id)));
			}

			return response;
		}
		#endregion
	}
}

