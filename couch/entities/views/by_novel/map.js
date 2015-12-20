function(doc) {
	if(doc.doc_type == 'Entity') {
		var novels = doc.novels;

		if(!novels)
			return;

		novels.forEach(function(novel) {
			emit([novel, doc.hint], doc);
		});
	}
}