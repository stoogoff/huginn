using System;
using Nancy;
using Huginn.Managers;
using Huginn.Exceptions;

namespace Huginn.Modules {
	public class NovelModule: ModelModule <Huginn.Models.Novel> {
		public NovelModule(): base("/novels") {
			manager = new NovelManager();

			Get["/{id}/chapters"] = parameters => {
				try {
					var id = parameters.id.ToString();

					return GetResponse((manager as NovelManager).Chapters(id));
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};
			Get["/{id}/entities"] = parameters => {
				try {
					var id = parameters.id.ToString();

					return GetResponse((manager as NovelManager).Entities(id));
				}
				catch(ServiceException se) {
					return GetResponse(se);
				}
			};
		}
	}
}

