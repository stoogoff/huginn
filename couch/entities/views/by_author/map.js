function(doc) {
	if(doc.doc_type == 'Entity' && !doc.trash)
		emit([doc.author, doc.hint], doc);
}