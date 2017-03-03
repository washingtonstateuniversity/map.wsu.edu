WSUApp.filter('getfieldname', function () {
	return function (field) {
		return field.type.name;
	};
}).filter('getfieldvalue', function () {
	return function (field) {
		var json = field.value;
		json = json.replace(/(\r\n|\n|\r)/gm, "");
		var fieldvalue = JSON.parse(json).selections[0].val;
		return fieldvalue;
	};
}).filter('unsafe', function ($sce) {
	return function (val) {
		return $sce.trustAsHtml(val);
	};
});