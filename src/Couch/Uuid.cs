using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huginn.Couch {
	public class Uuid {
		[JsonProperty("uuids")]
		public IList<string> Uuids { get; set; }
	}
}
