using Newtonsoft.Json;

namespace Huginn.Json {
	public class SerialiserSettings: JsonSerializerSettings {
		public const string Format = "yyyy-MM-ddTHH:mm:ssZ";

		public SerialiserSettings() {
			Formatting = Formatting.Indented;
			DateFormatString = Format;
			NullValueHandling = NullValueHandling.Ignore;
		}
	}
}

