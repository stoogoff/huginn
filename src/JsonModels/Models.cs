using System.Collections.Generic;
using Huginn.Data;

namespace Huginn.JsonModels {
	public interface IModel {

	}

	public class NovelJson: IModel {
		public Novel Novel { get; set; }
		public IList<Chapter> Chapters { get; set; }
		public IList<Entity> Entities { get; set; }
		public IList<Profile> Profiles { get; set; }
	}
	public class NovelsJson: IModel {
		public IList<Novel> Novels { get; set; }
	}

	public class ChapterJson: IModel {
		public Chapter Chapter { get; set; }
		public Novel Novel { get; set; }
	}
	public class ChaptersJson: IModel {
		public IList<Chapter> Chapters { get; set; }
	}

	public class EntityJson: IModel {
		public Entity Entity { get; set; }
		public IList<Novel> Novels { get; set; }
		public IList<ChapterSummary> Chapters { get; set; }
	}
	public class EntitiesJson: IModel {
		public IList<Entity> Entities { get; set; }
	}

	public class ProfileJson: IModel {
		public Profile Profile { get; set; }
		public IList<Novel> Novels { get; set; }
	}
	public class ProfilesJson: IModel {
		public IList<Profile> Profiles { get; set; }
		public IList<Novel> Novels { get; set; }
	}
}

