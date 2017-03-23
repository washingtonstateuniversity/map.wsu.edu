WSUApp.config(
	function(localStorageServiceProvider, $locationProvider)
	{
		$locationProvider.html5Mode({
			enabled: true,
			requireBase: false,
			rewriteLinks: false
		});
		localStorageServiceProvider.setPrefix('WSUApp');
	}
);