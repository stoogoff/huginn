using System;
using System.Collections.Generic;
using Nancy.Security;

namespace Huginn {
	public class HuginnUser: IUserIdentity {
		public int AuthorId { get; set; }
		public string UserName { get; set; }
		public IEnumerable<string> Claims { get; set; }
	}
}
