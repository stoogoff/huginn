// get all entities in each article
function(doc) {
	if(doc.doc_type != 'Article' && !doc.trash)
		return;

	var matches = doc.content.match(/{{([^\.]+)\.[^}]+}}/g);
	var hash = {}

	matches.forEach(function(match, index) {
		var key = match.replace('{{', '').substring(0, match.indexOf('.') - 2);

		if(!hash[key])
			hash[key] = 0;

		hash[key]++;
	});

	for(var key in hash) {
		emit([doc.author, key], { 'title': doc.title, 'id': doc._id, 'count': hash[key] });
	}
}