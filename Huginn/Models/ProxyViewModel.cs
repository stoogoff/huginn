using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	// it might be best to just use ViewModel
	[XmlRootAttribute("proxy")]
	public class ProxyViewModel {
		[JsonProperty("id")]
		[XmlAttribute("id")]
		public string Id { get; set; }

		[JsonProperty("object")]
		[XmlElement("object")]
		public string Object { get; set; }

		[JsonProperty("title")]
		[XmlElement("title")]
		public string Title { get; set; }
	}
}
