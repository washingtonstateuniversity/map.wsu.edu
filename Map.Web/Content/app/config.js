WSUApp.config(
	function($stateProvider, $httpProvider, localStorageServiceProvider, $urlRouterProvider)
	{
		//$httpProvider.defaults.cache = true;
		localStorageServiceProvider.setPrefix('WSUApp');
		//$urlRouterProvider.otherwise("/home");
		/*$stateProvider
			.state('elevator', {
				url: "/elevator",
				views: {
					"inside": {
						templateUrl: "app/views/elevator.html",
						controller: "AlertDetailsController"
					},
					"outside": {
						templateUrl: "app/views/alerts-outside.html"
					}
				}
			});*/
	}
);