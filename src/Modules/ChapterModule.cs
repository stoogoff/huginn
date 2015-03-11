using System;
using Huginn.Data;
using Huginn.Managers;

namespace Huginn.Modules {
	public class ChapterModule: ModelModule<Chapter> {
		public ChapterModule(): base("/chapters") {
			manager = new Manager<Chapter>("articles");
		}
	}
}
