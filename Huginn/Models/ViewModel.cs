using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	public class ViewModel {
		[JsonProperty("id")]
		[XmlAttribute("id")]
		public string Id { get; set; }

		[JsonProperty("revision")]
		[XmlAttribute("revision")]
		public string Revision { get; set; }

		[JsonProperty("created")]
		[XmlElement("created")]
		public DateTime Created { get; set; }

		[JsonProperty("modified")]
		[XmlElement("modified")]
		public DateTime Modified { get; set; }

		// XMLIgnore because that's what root nodes are for
		[JsonProperty("object")]
		[XmlIgnore]
		public string Object { get; protected set; }

		[JsonProperty("trash")]
		[XmlElement("trash")]
		public bool Trash { get; set; }
	}
}

