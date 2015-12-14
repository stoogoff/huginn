using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	using Huginn.Data;
	using Huginn.Extensions;

	// /books/<id>/profiles - IList<ProfileViewModel>
	// /profiles            - IList<ProfileViewModel>
	// /profiles/<id>       - ProfileViewModel
	[XmlRootAttribute("profile")]
	public class ProfileViewModel: ViewModel<Profile> {
		public ProfileViewModel(Profile profile): base(profile) { }

		// typography converted
		[JsonProperty("name")]
		[XmlElement("name")]
		public string Name {
			get {
				return data.Name.Prettify();
			}
		}

		// typography converted
		[JsonProperty("biography")]
		[XmlElement("biography")]
		public string Biography {
			get {
				return data.Biography.Prettify();
			}
		}
	}
}

