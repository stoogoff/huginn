using System;
using Huginn.Data;
using Huginn.Managers;

namespace Huginn.Modules {
	public class EntityModule: ModelModule<Entity> {
		public EntityModule(): base("/entities") {
			manager = new EntityManager();
		}
	}
}

