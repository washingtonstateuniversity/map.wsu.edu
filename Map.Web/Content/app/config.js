WSUApp.config(
	function($stateProvider, $httpProvider, localStorageServiceProvider, $urlRouterProvider)
	{
		localStorageServiceProvider.setPrefix('WSUApp');
	}
);