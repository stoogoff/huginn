function(doc) {
	if(doc.doc_type == 'Contributor' && !doc.trash)
		emit([doc.author, doc.name], doc);
}