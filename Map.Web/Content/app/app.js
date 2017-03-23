var WSUApp = angular.module('WSUApp', ['angular-click-outside', 'once', 'LocalStorageModule', 'ngAnimate', 'angular-iscroll', 'ngSanitize', 'ngTouch', 'uiGmapgoogle-maps'], function ($httpProvider) {
  // Use x-www-form-urlencoded Content-Type
  $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';

  var param = function(obj) {
    var query = '', name, value, fullSubName, subName, subValue, innerObj, i;

    for(name in obj) {
      value = obj[name];

      if(value instanceof Array) {
        for(i=0; i<value.length; ++i) {
          subValue = value[i];
          fullSubName = name + '[' + i + ']';
          innerObj = {};
          innerObj[fullSubName] = subValue;
          query += param(innerObj) + '&';
        }
      }
      else if(value instanceof Object) {
        for(subName in value) {
          subValue = value[subName];
          fullSubName = name + '[' + subName + ']';
          innerObj = {};
          innerObj[fullSubName] = subValue;
          query += param(innerObj) + '&';
        }
      }
      else if (value !== undefined && value !== null) {
		query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
      }
    }

    return query.length ? query.substr(0, query.length - 1) : query;
  };

  // Override $http service's default transformRequest
  $httpProvider.defaults.transformRequest = [function(data) {
    return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
  }];
})
.directive('loading', ['$http', function ($http) {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			scope.isLoading = function () {
				return $http.pendingRequests.length > 0;
			};
			scope.$watch(scope.isLoading, function (value) {
				if (value) {
					element.removeClass('ng-hide').addClass("open");
				} else {
					element.addClass('ng-hide').removeClass("open");
				}
			});
		}
	};
}]);

