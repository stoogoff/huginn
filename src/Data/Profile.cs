using System;
using Newtonsoft.Json;

namespace Huginn.Data {
	public class Profile: BaseData {
		[JsonProperty("doc_type")]
		public string DocType {
			get {
				return "Contributor";
			}
		}

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("biography")]
		public string Biography { get; set; }
	}
}
