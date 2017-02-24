WSUApp.factory('busService', function($q, $http, $timeout) {
    return {
        getBusList: function() {
			var deferred = $q.defer();
			$http.get("https://mobile.wsu.edu/WSUAppAdmin/home/getBusRouteList.castle").success(function(data)
			{
				deferred.resolve(data);
			}).error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
        },
		getPredictionDataForBusStop: function(stopid){
			var deferred = $q.defer();
			$http.get("https://mobile.wsu.edu/WSUAppAdmin/home/getPredictionDataForBusStop.castle?stopID="+stopid)
			.success(function(data)
			{
				deferred.resolve(data);
			})
			.error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
		},
		getPatternsForBusStop: function(stopid){
			var deferred = $q.defer();
			$http.get("https://mobile.wsu.edu/WSUAppAdmin/home/getPatternsForBusStop.castle?stopID="+stopid)
			.success(function(data)
			{
				deferred.resolve(data);
			})
			.error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
		},
		getBusLocation: function(busid) {
			var deferred = $q.defer();
			$http.get("https://mobile.wsu.edu/WSUAppAdmin/home/getBusLocation.castle?patternIds="+busid)
			.success(function(data)
			{
				deferred.resolve(data);
			})
			.error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
        },
        getRouteDetails: function(routeid) {
			var deferred = $q.defer();
			$http.get("https://mobile.wsu.edu/WSUAppAdmin/home/getBusRouteDetails.castle?patternId="+routeid)
			.success(function(data)
			{
				deferred.resolve(data);
			})
			.error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
        },
    };
});