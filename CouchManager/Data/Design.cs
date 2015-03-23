using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CouchManager.Data {
	public class Design {
		[JsonProperty("_id")]
		public string Id { get; set; }

		[JsonProperty("_rev")]
		public string Revision { get; set; }

		[JsonProperty("language")]
		public string Language {
			get {
				return "javascript";
			}
		}

		[JsonProperty("views")]
		public IDictionary<string, View> Views { get; set; }


		public void AddView(string name, View view) {
			if(Views == null) {
				Views = new Dictionary<string, View>();
			}

			Views.Add(name, view);
		}

		public void AddViews(IDictionary<string, View> views) {
			if(Views == null) {
				Views = new Dictionary<string, View>();
			}

			Views = views;
		}

		public void ClearViews() {
			if(Views != null) {
				Views.Clear();
			}
		}

		public static Design FromFolder(string path) {
			var design = new Design();
			var root = new DirectoryInfo(path);

			design.Id = "_design/" + root.Name;

			var views = Path.Combine(path, "views");

			foreach(var folder in Directory.GetDirectories(views)) {
				var view = View.FromFolder(folder);
				var info = new FileInfo(folder);

				design.AddView(info.Name, view);
			}

			return design;
		}
	}
}

