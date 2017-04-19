WSUApp.config(
	function (localStorageServiceProvider, $locationProvider, iScrollServiceProvider) {
		iScrollServiceProvider.configureDefaults({
			iScroll: {
				// Passed through to the iScroll library
				click: true
			},
			directive: {
				// Interpreted by the directive
				refreshInterval: 500
			}
		});
		$locationProvider.html5Mode({
			enabled: true,
			requireBase: false,
			rewriteLinks: false
		});
		localStorageServiceProvider.setPrefix('WSUApp');
	}
);