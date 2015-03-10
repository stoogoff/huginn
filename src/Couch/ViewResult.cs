using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huginn.Couch {
	public class ViewResult<T> {
		[JsonProperty("offset")]
		public int Offset { get; set; }

		[JsonProperty("total_rows")]
		public int TotalRows { get; set; }

		[JsonProperty("rows")]
		public IList<ResultRow<T>> Rows { get; set; }
	}

	public class ResultRow<T> {
		public string Id { get; set; }

		[JsonProperty("value")]
		public T Value { get; set; }
	}
}

