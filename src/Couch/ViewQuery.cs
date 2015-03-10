using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huginn.Couch {
	public class ViewQuery {
		[JsonProperty("descending")]
		public bool? Descending { get; set; }

		[JsonProperty("endkey")]
		public string EndKey { get; set; }

		[JsonProperty("group")]
		public bool? Group { get; set; }

		[JsonProperty("include_docs")]
		public bool? IncludeDocs { get; set; }

		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("limit")]
		public int? Limit { get; set; }

		[JsonProperty("reduce")]
		public bool? Reduce { get; set; }

		[JsonProperty("skip")]
		public int? Skip { get; set; }

		[JsonProperty("startkey")]
		public string StartKey { get; set; }

		// TODO this should probably be a general method
		public override string ToString() {
			var type = GetType();
			var properties = type.GetProperties();
			var buffer = new List<string>();

			foreach(var property in properties) {
				var value = property.GetValue(this);

				if(value == null)
					continue;

				string name = null;
				var attributes = property.CustomAttributes;

				foreach(var attribute in attributes) {
					if(attribute.AttributeType == typeof(Newtonsoft.Json.JsonPropertyAttribute)) {
						name = attribute.ConstructorArguments[0].Value.ToString();
						break;
					}
				}

				buffer.Add((name ?? property.Name) + "=" + Uri.EscapeUriString(value.ToString()));
			}

			return string.Join("&", buffer);
		}
	}
}

