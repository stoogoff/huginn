using System;

namespace Huginn.Services {
	public abstract class BaseService {
		private int authorId;

		protected BaseService(IDataRepository repo) {
			Repository = repo;
		}

		public IDataRepository Repository { get; private set; }

		public int AuthorId {
			get {
				return authorId;
			}
			set {
				authorId = value;

				if(Repository != null) {
					Repository.AuthorId = value;
				}
			}
		}
	}
}

