using System;
using Nancy;

namespace Huginn.Modules {
	using Huginn.Services;

	public class TestModule: NancyModule {
		protected readonly BookService service;

		public TestModule(): base("test") {
			service = new BookService(2);

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

