function(doc) {
	if(doc.doc_type == 'Novel' && !doc.trash) {
		if(!doc.contributors)
			return;

		doc.contributors.forEach(function(item, index, array) {
			emit([item, doc.title], doc);
		});
	}
}