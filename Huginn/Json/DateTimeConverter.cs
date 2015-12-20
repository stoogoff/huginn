using System;
using System.Collections.Generic;
using Nancy.Json;

namespace Huginn.Json {
	/*public class DateTimeConverter: JavaScriptPrimitiveConverter {
		public DateTimeConverter() { }

		public override IEnumerable<Type> SupportedTypes {
			get {
				yield return typeof(DateTime);
			}
		}

		public override object Serialize(object obj, JavaScriptSerializer serializer) {
			if(obj is DateTime) {
				var date = (DateTime) obj;

				return date.ToString(SerialiserSettings.Format);
			}

			return null;
		}

		public override object Deserialize(object primitiveValue, Type type, JavaScriptSerializer serializer) {
			if((type == typeof(DateTime)) && (primitiveValue is string)) {
				DateTime date;

				if(DateTime.TryParse(primitiveValue.ToString(), out date)) {
					return date;
				}
			}

			return null;
		}
	}*/
}

