using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Huginn.Extensions {
	using Huginn.Data;

	/*
	Typography could be done as extension methods to string:

	*/

	public static class StringExtensions {
		public static readonly Regex OpeningSingleQuote = new Regex("(^|[-\u2014\\s(\\[\"])'", RegexOptions.Compiled);

		/// <summary>
		/// Convert the following typographics elements:
		/// 	"
		/// 	'
		/// 	--
		/// 	---
		/// 	...
		/// </summary>
		/// <param name="content">The string input to convert.</param>
		public static string Prettify(this string content) {
			content = OpeningSingleQuote.Replace(content, "\\1\u2018");
				

				/*
			content = re.sub("(^|[-\u2014\s(\[\"])'",       "\\1\u2018", content) // opening singles
			content = re.sub("'",                           "\u2019",    content) // closing singles & apostrophes
			content = re.sub("(^|[-\u2014/\[(\u2018\s])\"", "\\1\u201c", content) // opening doubles
			content = re.sub("\"",                          "\u201d",    content) // closing doubles
			content = re.sub("---",                         "\u2014",    content) // em-dashes
			content = re.sub("--",                          "\u2013",    content) // en-dashes
			content = re.sub("\.\.\.",                      "\u2026",    content) // ellipsis
				*/

			return content;
		}

		public static string ParseEntities(this string input, IList<Entity> entities) {
			// apply entities to input string
			// include bad entity conversion
			return input;
		}

		/// <summary>
		/// Returns the word count of the text by counting available spaces.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="content">Content.</param>
		public static int WordCount(this string content) {
			return content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
		}
	}
}

