using Nancy;
using Huginn.Couch;
using Huginn.Json;
using Huginn.Exceptions;

namespace Huginn.Modules {
	public class SecurityModule: NancyModule {
		protected CouchClient client = new CouchClient("muninn");

		public SecurityModule(): this(string.Empty) {

		}
		public SecurityModule(string basePath): base(basePath) {
			Before += context => {
				var user = (context.CurrentUser as HuginnUser);

				if(user != null) {
					AuthorId = user.AuthorId;
					return null;
				}

				return ResponseHandler.GetResponse(new UnauthorisedException());
			};
		}

		public int AuthorId { get; protected set; }
	}
}

