﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Huginn.Data {
	public class Entity: CouchData {
		[JsonProperty("doc_type")]
		public string DocType {
			get {
				return "Entity";
			}
		}

		[JsonProperty("hint")]
		public string Hint { get; set; }

		[JsonProperty("notes")]
		public string Notes { get; set; }

		[JsonProperty("novels")]
		public IList<string> Books { get; set; }

		[JsonProperty("entities")]
		public IDictionary<string, string> Entities { get; set; }
	}
}

