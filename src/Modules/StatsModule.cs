using System;
using System.Collections.Generic;
using Nancy;
using Huginn.Couch;
using Huginn.Data;
using Huginn.Json;
using Huginn.Extensions;

namespace Huginn.Modules {
	public class StatsModule: SecurityModule {
		public StatsModule(): base("/stats") {
			Get["/objects"] = parameters => {
				var query = new ViewQuery {
					StartKey = ViewQuery.GetStartKey(AuthorId),
					EndKey = ViewQuery.GetEndKey(AuthorId),
					Group = true
				};
				var result = client.GetView<int>("stats", "objects_by_author", query);
				var response = new ValuesJson();

				foreach(var item in result.Rows) {
					// item.Key[0] is the author id, item.Key[1] is the type
					var key = item.Key[1].ToString();

					if(key == "Contributors")
						key = "Profiles";
					else if(key == "Articles")
						key = "Chapters";

					response.Add(key, item.Value);
				}

				return ResponseHandler.GetResponse(response);
			};

			Get["/months/{amount:int}"] = parameters => {
				var amount = parameters.amount;
				var end = DateTime.UtcNow;
				var start = end.AddMonths(-amount);

				return GetDateRange(start, end, "yyyy-MM", "by_month");
			};

			Get["/days/{amount:int}"] = parameters => {
				var amount = parameters.amount;
				var end = DateTime.UtcNow;
				var start = end.AddDays(-amount);

				return GetDateRange(start, end, "yyyy-MM-dd", "by_day");
			};

			Get["/last_write"] = parameters => {
				var query = new ViewQuery {
					StartKey = ViewQuery.GetStartKey(AuthorId),
					EndKey = ViewQuery.GetEndKey(AuthorId),
					Group = true
				};
				var result = client.GetView<long>("stats", "last_write", query);
				var response = new ValueJson<DateTime>();

				response.Value = result.Rows.Count > 0 ? result.Rows[0].Value.FromUnixTime() : DateTime.UtcNow;

				return ResponseHandler.GetResponse(response);
			};
		}

		protected Response GetDateRange(DateTime start, DateTime end, string dateFormat, string view) {
			var query = new ViewQuery {
				StartKey = "[" + AuthorId + ",\"" + start.ToString(dateFormat) + "\"]",
				EndKey = "[" + AuthorId + ",\"" + end.ToString(dateFormat) + "\"]",
				Group = true
			};

			var result = client.GetView<int>("stats", view, query);
			var response = new ValuesJson();

			// the entire date range needs to be sent back
			// convert to dictionary for easier lookup
			var dict = new Dictionary<string, int>();

			foreach(var item in result.Rows) {
				// item.Key[0] is the author id, item.Key[1] is the type
				dict.Add(item.Key[1].ToString(), item.Value);
			}

			while(start <= end) {
				var dateKey = start.ToString(dateFormat);
				var dateValue = 0;

				if(dict.ContainsKey(dateKey))
					dateValue = dict[dateKey];

				response.Add(dateKey, dateValue);

				if(view == "by_day")
					start = start.AddDays(1);
				else
					start = start.AddMonths(1);
			}

			return ResponseHandler.GetResponse(response);
		}
	}
}

