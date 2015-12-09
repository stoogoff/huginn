using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	// /books/<id>/profiles - IList<ProfileViewModel>
	// /profiles            - IList<ProfileViewModel>
	// /profiles/<id>       - ProfileViewModel
	[XmlRootAttribute("profile")]
	public class ProfileViewModel: ViewModel {
		public ProfileViewModel() {
			Object = "profile";
		}

		// typography converted
		[JsonProperty("name")]
		[XmlElement("name")]
		public string Name { get; set; }

		// full Markdown content which has gone through the following process:
		//	- entities replaced
		//	- typography converted
		[JsonProperty("biography")]
		[XmlElement("biography")]
		public string Biography { get; set; }
	}
}

