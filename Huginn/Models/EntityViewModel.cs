using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	// /books/<id>/entities    - IList<EntityViewModel>
	// /chapters/<id>/entities - IList<EntityViewModel>
	// /entities               - IList<EntityViewModel>
	// /entities/<id>          - EntityViewModel
	[XmlRootAttribute("entity")]
	public class EntityViewModel: ViewModel {
		public EntityViewModel() {
			Object = "entity";
		}

		[JsonProperty("name")]
		[XmlElement("name")]
		public string Hint { get; set; }

		// tyopgraphy converted
		[JsonProperty("notes")]
		[XmlElement("notes")]
		public string Notes { get; set; }

		[JsonProperty("entities")]
		[XmlElement("entities")]
		public IDictionary<string, string> Entities { get; set; }
	}
}

