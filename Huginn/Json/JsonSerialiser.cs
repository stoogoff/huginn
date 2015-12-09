using System;
using System.IO;
using System.Collections.Generic;
using Nancy;
using Newtonsoft.Json;

namespace Huginn.Json {
	public class JsonNetSerialiser: ISerializer {
		private readonly JsonSerializer serialiser;

		public JsonNetSerialiser() {
			serialiser = JsonSerializer.Create(new SerialiserSettings());
		}

		#region ISerializer implementation
		public bool CanSerialize(string contentType) {
			return contentType == "application/json";
		}

		public void Serialize<TModel>(string contentType, TModel model, Stream outputStream) {
			using (var writer = new JsonTextWriter(new StreamWriter(outputStream))) {
				serialiser.Serialize(writer, model);
				writer.Flush();
			}
		}

		public IEnumerable<string> Extensions {
			get {
				yield return "json";
			}
		}
		#endregion
	}
}

