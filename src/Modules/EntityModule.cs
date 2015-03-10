using System;

namespace Huginn.Modules {
	public class EntityModule: ModelModule <Huginn.Models.Entity> {
		public EntityModule(): base("/entities") {
		}
	}
}

