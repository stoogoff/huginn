using System.IO;
using Newtonsoft.Json;

namespace CouchManager.Data {
	public class View {
		[JsonProperty("map")]
		public string Map { get; set; }

		[JsonProperty("reduce")]
		public string Reduce { get; set; }

		public static View FromFolder(string path) {
			var view = new View();
			var files = Directory.GetFiles(path);

			foreach(var file in files) {
				var info = new FileInfo(file);
				string contents = null;

				using(var reader = info.OpenText()) {
					contents = reader.ReadToEnd();
				}

				if(!string.IsNullOrEmpty(contents)) {
					switch(info.Name) {
						case "map.js":
							view.Map = contents;
							break;

					case "reduce.js":
							view.Reduce = contents;
							break;
					}
				}
			}

			return view;
		}
	}
}

