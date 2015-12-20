using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Huginn.Extensions {
	using Huginn.Data;

	/*
	Typography could be done as extension methods to string:

	*/

	public static class StringExtensions {
		private static readonly Regex EntityParse = new Regex("{{([^}]+)}}", RegexOptions.Compiled);
		private static readonly Regex OpeningSingleQuote = new Regex("(^|[-\u2014\\s(\\[\"])'", RegexOptions.Compiled);
		private static readonly Regex OpeningDoubleQuote = new Regex("(^|[-\u2014/\\[(\u2018\\s])\"", RegexOptions.Compiled);

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
			if(string.IsNullOrEmpty(content)) {
				return string.Empty;
			}

			content = OpeningSingleQuote.Replace(content, "$1\u2018"); // opening singles
			content = content.Replace('\'',               '\u2019');   // closing singles & apostrophes
			content = OpeningDoubleQuote.Replace(content, "$1\u201c"); // opening doubles
			content = content.Replace('"',                '\u201d');   // closing doubles
			content = content.Replace("---",              "\u2014");   // em-dashes
			content = content.Replace("--",               "\u2013");   // en-dashes
			content = content.Replace("...",              "\u2026");   // ellipsis

			return content;
		}

		public static string ParseEntities(this string content, IList<Entity> entities) {
			if(string.IsNullOrEmpty(content)) {
				return string.Empty;
			}

			if(entities == null) {
				return content;
			}

			// convert the entity list to a look up table
			var lookup = new Dictionary<string, IDictionary<string, string>>();

			foreach(var entity in entities) {
				lookup.Add(entity.Hint, entity.Entities);
			}

			// simple parse of the entities
			// if the entity value can't be found the raw syntax is returned
			// it's up to clients as to how they want to handle this
			content = EntityParse.Replace(content, match => {
				// shouldn't happen unless the regex gets changed
				if(match.Groups.Count < 2) {
					return match.Value;
				}

				var source = match.Groups[1].ToString().Split('.');

				// malformed property selector
				if(source.Length != 2) {
					return match.Value;
				}

				// key doesn't exist in the entity list
				if(!lookup.ContainsKey(source[0])) {
					return match.Value;
				}

				var entity = lookup[source[0]];

				// the entity property doesn't exist
				if(!entity.ContainsKey(source[1])) {
					return match.Value;
				}

				return entity[source[1]];
			});

			return content;
		}

		/// <summary>
		/// Returns the word count of the text by counting available spaces.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="content">Content.</param>
		public static int WordCount(this string content) {
			if(string.IsNullOrEmpty(content)) {
				return 0;
			}

			return content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
		}
	}
}

