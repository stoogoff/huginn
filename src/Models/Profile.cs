using System;
using Newtonsoft.Json;

namespace Huginn.Models {
	public class Profile: BaseModel {
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("biography")]
		public string Biography { get; set; }
	}
}
