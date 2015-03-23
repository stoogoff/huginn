function(doc) {
	if(doc.doc_type && doc.doc_type != "Stats") {
		var type = doc.doc_type + "s";

		if(type.match(/ys$/))
			type = type.replace(/ys$/, 'ies');

		emit([doc.author, type], 1);
	}
}