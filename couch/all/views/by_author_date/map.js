function(doc) {
	if(doc.doc_type != 'Stats' && !doc.trash) {
		// date regex match
		var match = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})/;
		var modified = doc.modified.match(match);
		var date = new Date(modified[1], modified[2] - 1, modified[3], modified[4], modified[5], modified[6]);

		var obj = {
			"_id": doc._id,
			"_rev": doc._rev,
			"modified": doc.modified,
			"created": doc.created,
			"doc_type": doc.doc_type,
			"author": doc.author,
			"title": "Unknown Document"
		};

		["title", "hint", "name"].forEach(function(n) {
			if(doc[n])
				obj.title = doc[n];
		});

		emit([doc.author, -date], obj);
	}
}