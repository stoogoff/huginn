using Nancy;

namespace Huginn.Modules {
	using Huginn.Exceptions;
	using Huginn.Services;

	public class TestModule: NancyModule {
		protected readonly IBookService service;

		public TestModule(IBookService service): base("test") {
			this.service = service;

			Before += context => {
				var user = (context.CurrentUser as HuginnUser);

				if(user != null) {
					service.AuthorId = user.AuthorId;
					return null;
				}

				return "Unauthorized";
			};

			// index of T
			Get["/"] = parameters => service.All();

			// single T based on ID
			Get["/{id}"] = parameters => {
				var id = parameters.id.ToString();

				return service.Get(id);
			};
		}
	}
}

