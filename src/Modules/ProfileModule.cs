using System;

namespace Huginn.Modules {
	public class ProfileModule: ModelModule<Huginn.Models.Profile> {
		public ProfileModule(): base("/profiles") {

		}
	}
}

