using System.Collections.Generic;

namespace Huginn.Extensions {
	using Huginn.Data;

	/*
	Typography could be done as extension methods to string:

	*/

	public static class StringExtensions {
		public static string Prettify(this string input) {
			// apply typographic conversions
			return input;
		}

		public static string ParseEntities(this string input, IList<Entity> entities) {
			// apply entities to input string
			// include bad entity conversion
			return input;
		}
	}
}

