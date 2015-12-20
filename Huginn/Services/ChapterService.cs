using System;
using System.Collections.Generic;

namespace Huginn.Services {
	using Huginn.Data;
	using Huginn.Models;

	public interface IChapterService: IModelViewService<UnparsedChapterViewModel, Chapter> {
		BookViewModel Book(string id);
		IList<EntityViewModel> Entities(string id);
	}

	public class ChapterService: BaseService, IChapterService {
		public ChapterService(IDataRepository repo): base(repo) { }

		#region IModelViewService
		public IList<UnparsedChapterViewModel> All() {
			var chapters = Repository.AllObjects<Chapter>("articles");
			var response = new List<UnparsedChapterViewModel>();

			Console.WriteLine(chapters.Count);

			// loop through books and convert to BookViewModels
			foreach(var chapter in chapters) {
				response.Add(new UnparsedChapterViewModel(chapter));
			}

			return response;
		}

		public UnparsedChapterViewModel Get(string id) {
			var chapter = Repository.GetObject<Chapter>(id);

			return new UnparsedChapterViewModel(chapter);
		}

		public UnparsedChapterViewModel Create(Chapter data) {
			var chapter = Repository.CreateObject(data);

			// TODO set sort order based on novel
			// TODO update word count stats

			return new UnparsedChapterViewModel(chapter);
		}

		public UnparsedChapterViewModel Save(string id, Chapter data) {
			var chapter = Repository.SaveObject(id, data);

			// TODO update word count stats

			return new UnparsedChapterViewModel(chapter);
		}
		#endregion

		#region implement IChapterService
		public BookViewModel Book(string id) {
			var chapter = Repository.GetObject<Chapter>(id);
			var book = Repository.GetObject<Book>(chapter.Book);
			var entities = GetEntitiesForBook(book.Id);

			return new BookViewModel(book, entities);
		}

		public IList<EntityViewModel> Entities(string id) {
			var chapter = Repository.GetObject<Chapter>(id);

			// get entities for book
			var entities = GetEntitiesForBook(chapter.Book);
			var response = new List<EntityViewModel>();

			foreach(var entity in entities) {
				response.Add(new EntityViewModel(entity));
			}

			return response;
		}
		#endregion
	}
}

