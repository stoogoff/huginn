function(doc) {
	if(doc.trash) {
		// date regex match
		var match = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})/;
		var created = doc.created.match(match);
		var date = new Date(created[1], created[2] - 1, created[3], created[4], created[5], created[6]);

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