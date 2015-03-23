﻿using System;

namespace Huginn.Extensions {
	public static class DateTimeExtensions {
		public static DateTime FromUnixTime(this long unixTime) {
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

			return epoch.AddMilliseconds(unixTime);
		}
	}
}

