WSUApp.factory('myCache', function($cacheFactory) {
 return $cacheFactory('myData');
}).factory('mapService', function($q, $http, $timeout, myCache) {
    return {
		getCategoryList: function() {
			var deferred = $q.defer();
			$http.get("/api/v1/category/").then(
                function (response)
				{
                    deferred.resolve(response.data);
                },
                function (response)
                {
                    deferred.reject(response.data);
                });
			return deferred.promise;
        },
		getPlaceObjByCategories: function(ids, campusid) {
			var deferred = $q.defer();
			var url = '/api/v1/category/places?ids=' + ids + '&campusid=' + campusid;
			if (ids === null) {
				deferred.resolve([]);
			}
			$http.get(url).then(function (response)
				{
					deferred.resolve(response.data);
				},
                function (response)
                {
                    deferred.reject(response.data);
                }
            );
			return deferred.promise;
        },
		getPlaceById: function(id) {
			var deferred = $q.defer();
			$http.get("/api/v1/place/" + id)
                .then(function (response)
				{
					deferred.resolve(response.data);
                },
                function (response)
                {
                    deferred.reject(response.data);
                });
			return deferred.promise;
        },
		keyWordAutoComplete: function(val) {
			var deferred = $q.defer();
			$http.get("/api/v1/place/search/?query=" + val)
                .then(function (response)
				{
					deferred.resolve(response.data);
                },
                function (response)
                {
                    deferred.reject(response.data);
                });
			return deferred.promise;
		},
		getSmallUrl: function (url) {
			var deferred = $q.defer();
			$http.get("/api/v1/url/?url=" + encodeURIComponent(url))
			.then(function (response) {
				deferred.resolve(response.data);
				},
				function (response) {
					deferred.reject(response.data);
			});
			return deferred.promise;
		},
		reportError: function(data) {
			console.info("reportError", data);
			var deferred = $q.defer();
			$http({
				method: 'GET',
				url: "/api/v1/email/",
				params: data
			})
			.then(function (response) {
				deferred.resolve(response.data);
			},
			function (response) {
				deferred.reject(response.data);
			});
			return deferred.promise;
        }
    };
});