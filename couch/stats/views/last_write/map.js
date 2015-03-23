function(doc) {
	if(doc.doc_type == 'Stats') {
		// date regex match
		var match = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})/;
		var created = doc.created.match(match);
		var date = new Date(created[1], created[2] - 1, created[3], created[4], created[5], created[6]);

		emit([doc.author], date.getTime());
	}
}