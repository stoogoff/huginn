function(doc) {
	if(doc.doc_type == 'Article' && !doc.trash)
		emit(doc.author, doc);
}