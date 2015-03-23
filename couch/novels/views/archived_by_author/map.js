function(doc) {
	if(doc.doc_type == 'Novel' && !doc.trash && doc.archive)
		emit([doc.author, doc.title], doc);
}