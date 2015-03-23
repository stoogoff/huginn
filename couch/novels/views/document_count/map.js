function(doc) {
	if(doc.doc_type == 'Article' && !doc.trash && !doc.archive)
		emit([doc.author, doc.novel], 1);
}