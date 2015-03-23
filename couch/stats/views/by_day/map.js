function(doc) {
	if(doc.doc_type == 'Stats') {
		// date regex match
		var match = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})/;
		var created = doc.created.match(match);
		var date = created[1] + "-" + created[2] + "-" + created[3];

		emit([doc.author, date], doc.wordcount);
	}
}