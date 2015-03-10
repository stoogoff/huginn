using System;

namespace Huginn.Modules {
	public class TestModule: ModelModule <Huginn.Models.Test> {
		public TestModule(): base("/test") {}
	}
}

