using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	using Huginn.Data;
	using Huginn.Extensions;
	
	// /books/<id>/entities    - IList<EntityViewModel>
	// /chapters/<id>/entities - IList<EntityViewModel>
	// /entities               - IList<EntityViewModel>
	// /entities/<id>          - EntityViewModel
	[XmlRootAttribute("entity")]
	public class EntityViewModel: ViewModel<Entity> {
		public EntityViewModel(Entity entity): base(entity) { }

		[JsonProperty("name")]
		[XmlElement("name")]
		public string Hint {
			get {
				return data.Hint;
			}
		}

		// tyopgraphy converted
		[JsonProperty("notes")]
		[XmlElement("notes")]
		public string Notes {
			get {
				return data.Notes.Prettify();
			}
		}

		[JsonProperty("entities")]
		[XmlElement("entities")]
		public IDictionary<string, string> Entities {
			get {
				return data.Entities;
			}
		}
	}
}

