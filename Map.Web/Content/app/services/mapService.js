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
		getPlaceObj: function(id) {
			var deferred = $q.defer();
			$http.get('https://map.wsu.edu/public/get_place_obj.castle?id=' + id).then(function (data)
			{
				deferred.resolve(data);
			}).error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
        },
		getPlaceByKeyword: function(name) {
			var deferred = $q.defer();
			$http.get('https://map.wsu.edu/public/get_place_by_keyword.castle?str=' + name).then(function (data)
			{
				deferred.resolve(data);
			}).error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
        },
		getPlaceByCategoryName: function(name) {
			var deferred = $q.defer();
			$http.get('https://map.wsu.edu/public/get_place_by_categoryname.castle?catname=' + encodeURIComponent(name)).then(function (data)
			{
				deferred.resolve(data);
			}).error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
        },
		getPlaceObjByCategories: function(ids) {
			var deferred = $q.defer();
			var url = '/api/v1/category/places?ids=' + ids;
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
		getAllPlaces: function() {
			logInfo('getting all places');
			var allcats = "academics,agriculture,art_music_design,bio_phys_math_sci,business_econ,communication,education,engineering,health_sciences,hist_lit_lang_philosophy,social_sciences,sport_fitness,veterinary_medicine,libraries_computer_labs,tutoring_support,arts_culture,museums_exhibits,performing_arts,outdoor_sculpture,monuments_sculpture,athletics_recreation,sports_venues,recreation,athletic_training,entertainment_venues,food_shopping,espresso_snacks,dining,shopping,convenience,housing,residence_halls,co_ed,men_only,women_only,age_specific,apartments,single_student,family_graduate,landmarks,services_admin,student_services,staff_services,central_admin,utilities_maint,parking,bus_routes,cougprints,gameday-park-n-Ride,gameday-park-n-ride-employee,blue1,orange1,orange2,orange3,orange4,green1,green2,green3,yellow1,yellow3,yellow4,red1,red2,red3,crimson1,crimson2,crimson3,gray1,gray2,publicpayparking,visitorpermits,pedestrianmallarea,disabilityparking,greenbike,parkridelot,zipcar,meteredparking";
			var url = "https://map.wsu.edu/public/get_place_by_category.castle?cat[]="+allcats;
			var deferred = $q.defer();
			$http({ cache: true, url: url, method: 'GET' }).then(function (data)
			{
				deferred.resolve(data);
			}).error(function(data,status){ deferred.reject(data); });
			return deferred.promise;
        },
		getPlaceByCategory: function(name) {
			var deferred = $q.defer();
			$http({ cache: false, url: 'https://map.wsu.edu/public/get_place_by_category.castle?cat[]=' + encodeURIComponent(name), method: 'GET' }).then(function (data)
			{
				deferred.resolve(data);
			}).error(function(data,status){ deferred.reject(data); });
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
                function (reponse) 
                {
                    deferred.reject(response.data);
                });
			return deferred.promise;
        },
		reportError: function(data) {
			console.info("reportError", data);
			var deferred = $q.defer();
			$http.post("https://map.wsu.edu/public/reporterror.castle", data)
			.success(function (data, status, headers, config) {
				deferred.resolve(data);
			})
			.error(function (data, status, header, config) {
				deferred.reject(data);
			});
			return deferred.promise;	
        }
    };
});