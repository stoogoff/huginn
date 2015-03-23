function(key, values, rereduce) {
	var total = 0;

	values.forEach(function(value) {
		if(value > total)
			total = value;
	});

	return total;
}