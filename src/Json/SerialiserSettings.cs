using Newtonsoft.Json;

namespace Huginn.Json {
	public class SerialiserSettings: JsonSerializerSettings {
		public const string Format = "yyyy-MM-ddTHH:mm:ssZ";

		public SerialiserSettings() {
			DateFormatString = Format;
			NullValueHandling = NullValueHandling.Ignore;
		}
	}
}

