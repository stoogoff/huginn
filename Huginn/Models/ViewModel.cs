using System;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Huginn.Models {
	using Huginn.Data;

	public class ViewModel<T> where T: CouchData {
		protected T data;

		public ViewModel(T data) {
			this.data = data;
		}


		[JsonProperty("id")]
		[XmlAttribute("id")]
		public string Id {
			get {
				return data.Id;
			}
		}

		[JsonProperty("revision")]
		[XmlAttribute("revision")]
		public string Revision {
			get {
				return data.Revision;
			}
		}

		[JsonProperty("created")]
		[XmlElement("created")]
		public DateTime Created {
			get {
				return data.Created;
			}
		}

		[JsonProperty("modified")]
		[XmlElement("modified")]
		public DateTime Modified {
			get {
				return data.Modified;
			}
		}

		// XMLIgnore because that's what root nodes are for
		[JsonProperty("object")]
		[XmlIgnore]
		public string Object {
			get {
				// switch or map
				/*switch(typeof(T)) {
				case Book:
					return "book";
				case Entity:
					return "entity";
				case Profile:
					return "profile";
				case Chapter:
					return "chapter";
				}*/

				return "object";
			}
		}

		[JsonProperty("trash")]
		[XmlElement("trash")]
		public bool Trash {
			get {
				return data.Trash;
			}
		}
	}
}

