using System.Collections.Generic;
using Huginn.Data;

namespace Huginn.Json {
	public interface IModel {

	}

	public class BookJson: IModel {
		public Book Book { get; set; }
		public IList<Chapter> Chapters { get; set; }
		public IList<Entity> Entities { get; set; }
		public IList<Profile> Profiles { get; set; }
	}
	public class BooksJson: IModel {
		public IList<Book> Books { get; set; }
	}

	public class ChapterJson: IModel {
		public Chapter Chapter { get; set; }
		public Book Book { get; set; }
		public IList<Entity> Entities { get; set; }
	}
	public class ChaptersJson: IModel {
		public IList<Chapter> Chapters { get; set; }
	}

	public class EntityJson: IModel {
		public Entity Entity { get; set; }
		public IList<Book> Books { get; set; }
		public IList<ChapterSummary> Chapters { get; set; }
	}
	public class EntitiesJson: IModel {
		public IList<Entity> Entities { get; set; }
	}

	public class ProfileJson: IModel {
		public Profile Profile { get; set; }
		public IList<Book> Books { get; set; }
	}
	public class ProfilesJson: IModel {
		public IList<Profile> Profiles { get; set; }
		public IList<Book> Books { get; set; }
	}
	public class ValuesJson: IModel {
		public IList<KeyValue<string, int>> Values { get; set; }

		public void Add(string key, int value) {
			if(Values == null) {
				Values = new List<KeyValue<string, int>>();
			}

			Values.Add(new KeyValue<string, int> {
				Key = key,
				Value  = value
			});
		}
	}
	public class ValueJson<T>: IModel {
		public T Value { get; set; }
	}
	public class KeyValue<K, V> {
		public K Key { get; set; }
		public V Value { get; set; }
	}
}

