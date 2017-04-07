(function () {
	'use strict';

	WSUApp.controller('MainCtrl', function ($scope, $q, $http, $location, $rootScope, $timeout, $interval, $window, localStorageService, mapService, uiGmapIsReady, iScrollService) {
		// for iscroll
		var vm = this;  // Use 'controller as' syntax
		vm.iScrollState = iScrollService.state;

		// The official array of markers
		$scope.markers = [];

		$scope.useragent = navigator.userAgent;
		$scope.currentUrl = window.location.href;
		$scope.embedW = 495;
		$scope.embedH = 372;
		$scope.showPrintable = false;
		$scope.showDirections = false;
		$scope.ismobile = jQuery(window).width() < 990;

		$scope.$watch(function() {
			return $location.path();
		}, function(){
			$scope.currentUrl = window.location.href;
		});

		// unclick any open menu items
		$scope.clearTheMenu = function () {
			for (var i = 0; i < $scope.categories.length; i++) {
				$scope.categories[i].active = false;
			}
		};

		// recenter map on pullman
		$scope.recenterMap = function () {
			$scope.map.center.latitude = center_lat;
			$scope.map.center.longitude = center_lng;
			$scope.map.zoom = defaultzoom;
		};

		// get category by name
		$scope.getCategoryByName = function (searchname) {
			for (var i = 0; i < $scope.categories.length; i++) {
				if ($scope.categories[i].name === searchname || $scope.categories[i].friendly_name === searchname || $scope.categories[i].id === parseInt(searchname)) {
					return $scope.categories[i];
				}
			}
		};
		$scope.setMenuActive = function (category) {
			for (var i = 0; i < $scope.categories.length; i++) {
				if ($scope.categories[i].id === category.id) {
					$scope.categories[i].active = true;
				}
			}
		};
		$scope.getActiveMenuLinks = function () {
			var activeids = null;
			for (var i = 0; i < $scope.categories.length; i++) {
				if ($scope.categories[i].active) {
					if (activeids) {
						activeids += "," + $scope.categories[i].id;
					}
					else {
						activeids = $scope.categories[i].id;
					}
				}
			}
			return activeids;
		};
		$scope.getActiveMenuLinksNoParents = function () {
			var activeids = null;
			for (var i = 0; i < $scope.categories.length; i++) {
				if ($scope.categories[i].active && $scope.categories[i].Parent !== null) {
					if (activeids) {
						activeids += "," + $scope.categories[i].id;
					}
					else {
						activeids = $scope.categories[i].id;
					}
				}
			}
			if (activeids === null)
			{
				activeids = $scope.getActiveMenuLinks();
			}
			return activeids;
		};

		$scope.clickedMenuItem = function (clickedCategory) {
			$scope.toggleActiveMenuItem(clickedCategory);
			$scope.setShortUrl();
		};

		// They clicked a menu item
		$scope.toggleActiveMenuItem = function (clickedCategory) {
			var deferred = $q.defer();
			$scope.clearShapes();
			$scope.closeWindow();

			if (clickedCategory) {
				if (clickedCategory.Parent === null) {
					$scope.clearTheMenu();
					clickedCategory.active = !clickedCategory.active;
				}
				else {
					// If we need to set the parent to active in the menu
					if (clickedCategory.Parent !== null) {
						$scope.setMenuActive(clickedCategory.Parent);
					}
					// Toggle the clicked category in the menu
					clickedCategory.active = !clickedCategory.active;
				}
			}

			// Get all menu links but parent categories would include everything
			var activeids = $scope.getActiveMenuLinksNoParents();

			// If they've unclicked all sub categories
			if (!activeids) {
				$scope.clearTheMenu();
			}
			else {
				mapService.getPlaceObjByCategories(activeids,$scope.campusid).then(
					function (markers) {
						$scope.markers = markers;
						for (var i = 0; i < $scope.markers.length; i++) {
							$scope.prepMarker($scope.markers[i], i + 1);
						}
						$scope.showShapes();
						$scope.openListings();
						$scope.resetAllScrollers($rootScope.myScroll, 250);
						deferred.resolve();
					}
				);
			}
			return deferred.promise;
		};

		// Get the equivalent url params like ?cat[]=2,3,4&pid=1 where pid is the clicked place and cat[] is the categories open
		$scope.getURLParams = function (categories, pid) {
			var newurl = "?cat[]=" + categories;
			if (pid)
			{
				newurl += "&pid=" + pid;
			}
			newurl += "&campusid=" + $scope.campusid;
			return newurl;
		};

		// Get short url from Database like /t/350E1FC4 and set it in the URL
		$scope.setShortUrl = function () {
			var activeids = $scope.getActiveMenuLinks();
			var placeid = null;
			if ($scope.map.window.model) {
				placeid = $scope.map.window.model.id;
			}
			var newurl = $scope.getURLParams(activeids, placeid);
			if (placeid === null && activeids === null) {
				$window.history.pushState({ cat: activeids, pid: placeid, campusid: $scope.campusid }, null, "/");
			}
			else {
				return mapService.getSmallUrl(newurl).then(function (data) {
					$window.history.pushState({ cat: activeids, pid: placeid, campusid: $scope.campusid }, newurl, "/t/" + data.sm_url);
				});
			}
		};

		// Update the page content when the popstate event is called, i.e. back button pushed
		$window.addEventListener('popstate', function (event) {
			if (event && event.state) {
				var newcategories = event.state.cat;
				var placeid = event.state.pid;
				var campusid = event.state.campusid;
				setupSavedCategoriesAndPlaceMap(newcategories.toString(), placeid, campusid);
			}
		});

		// We get a marker from JSON, but we need to arrange it to use it on the google map
		$scope.prepMarker = function (marker, i) {
			var width = parseInt(30);
			var height = parseInt(50);

			marker.icon = {
				url: '/api/v1/marker/' + i,
				size: new google.maps.Size(width, height),
				scaledSize: new google.maps.Size(width, height),
				origin: new google.maps.Point(0, 0),
				anchor: new google.maps.Point(width / 2, height / 2)
			};
			marker.position = {};
			marker.position.latitude = marker.latitude;
			marker.position.longitude = marker.longitude;
			marker.number = i;
			marker.markeroptions = {
				optimized: false
			};
		};

		// Given a place id, load it
		$scope.loadOnePlace = function (id) {
			mapService.getPlaceById(id).then(function (marker) {
				$scope.prepMarker(marker, 1);
				console.info("getplacebyid", marker);
				$scope.markers = [];
				$scope.markers.push(marker);
				if (marker) {
					$scope.showWindow(marker);
				}
				$scope.closeListings();
				$scope.clearTheMenu();
			});
			$rootScope.searchplaces = [];
			$scope.searchterm = "";
		};

		// Handle opening and closing the listings panel
		var maxpanelwidth = "235px";
		$scope.openListings = function () {
			// Close the menu
			if ($scope.ismobile && $(".spine-mobile-open").length > 0) {
				$("#shelve").click();
			}

			jQuery("#selectedPlaceList").animate({ width: '235px' }).addClass("active");

			$scope.listingsopen = true;
			$timeout($scope.setMapDimensions, 400);
		};

		// Close marker listings window
		$scope.closeListings = function () {
			var width = parseInt(jQuery("#selectedPlaceList").width());
			if (width > 0) {
				jQuery("#selectedPlaceList").animate({ width: 0 }).removeClass("active");
				$scope.listingsopen = false;
			}

			$timeout($scope.setMapDimensions, 400);
			$timeout($scope.setMapDimensions, 2400);
		};

		// Toggle listings open/closed
		$scope.toggleListings = function () {
			var selectedPlaceListWidth = parseInt(jQuery("#selectedPlaceList").width());
			if (selectedPlaceListWidth > 0) {
				$scope.closeListings();
			}
			else {
				$scope.openListings();
			}
		};

		// Handle clicking of either the icon on the map or the listing in the listing panel
		$scope.showWindow = function (selectedPlace) {
			console.debug("selectedPlace", selectedPlace);
			$scope.map.window.model = selectedPlace;
			$scope.setShortUrl();
			$scope.map.window.show = true;
			$scope.centerOnPopupWindow();
			$scope.mapinstance.setOptions({ scrollwheel: false });

			$timeout(setupInfoWindow, 1200);
			$scope.showShape(selectedPlace);
		};

		$scope.centerOnPopupWindow = function () {
			var centerpoint = new google.maps.LatLng($scope.map.window.model.position.latitude, $scope.map.window.model.position.longitude);
			$scope.mapinstance.setCenter(centerpoint);
		};

		$scope.showShapes = function () {
			$scope.map.polys = [];
			for (var i = 0; i < $scope.markers.length; i++) {
				$scope.showShape($scope.markers[i]);
			}
		};

		$scope.showShape = function (place) {
			if (place.shapes.length > 0 && place.shapes[0].encoded) {
				var decodedPath = google.maps.geometry.encoding.decodePath(place.shapes[0].encoded);
				var events = JSON.parse(place.shapes[0].style[0].style_obj).events;
				var strokecolor = events.rest.strokeColor;
				var strokeopacity = events.rest.strokeOpacity;
				var fillcolor = events.rest.fillColor;
				var fillopacity = events.rest.fillOpacity;
				var strokeweight = events.rest.strokeWeight;

				place.shapes[0].fill = { "color": fillcolor, "opacity": fillopacity };
				place.shapes[0].stroke = { "color": strokecolor, "weight": strokeweight, "opacity": strokeopacity };
				place.shapes[0].path = decodedPath;
				for (var i = 0; i < place.shapes[0].path.length; i++) {
					place.shapes[0].path[i].latitude = place.shapes[0].path[i].lat();
					place.shapes[0].path[i].longitude = place.shapes[0].path[i].lng();
				}
				place.shapes[0].id = place.id;
				place.shapes[0].editable = true;

				$scope.map.polys.push(place.shapes[0]);
			}
		};

		// Some defaults
		var center_lat = map_view.campus_latlng_str.split(',')[0];
		var center_lng = map_view.campus_latlng_str.split(',')[1];
		var spinewidth = 0;
		var defaultzoom = 15;
		$scope.listingsopen = false;

		$scope.setMapDimensions = function (extrawidth) {
			var markerlistingswidth = 0;

			if ($scope.listingsopen && $scope.markers.length > 0) {
				markerlistingswidth = jQuery("#selectedPlaceList").width();
			}

			var spineheader = $(".spine-header").height();
			var placesearch = $("#placeSearch").height();
			var spinefooter = $(".spine-footer").height();
			var windowheight = $(window).height();

			var isMobile = $scope.ismobile;
			var mapHeight = $(window).height() - $("#header_bar").height() - 4;

			if (isMobile) {
				mapHeight -= $(".spine-header").height();
				spinewidth = 0;
			}
			else {
				spinewidth = $("#spine").width();
			}

			$(".angular-google-map-container, .heightminusbar").height(mapHeight);
			$(".angular-google-map-container").width($(window).width() - spinewidth - markerlistingswidth);
			$(".windowheight").height(windowheight);
			$(".navheight").height(windowheight - spineheader - placesearch - spinefooter);

			if ($scope.mapinstance) {
				google.maps.event.trigger($scope.mapinstance, 'resize');
				$scope.fitToBounds();
			}
		};

		// Handle window resize
		$(window).resize(function () {
			$scope.ismobile = jQuery(window).width() < 990;
			$scope.setMapDimensions();
		}).trigger("resize");

		// A function to resize google map to include all markers on the map
		$scope.fitToBounds = function () {
			if ($scope.markers.length > 0) {
				if ($scope.map.window.model) {
					$scope.centerOnPopupWindow();
				}
				else {
					var bounds = new google.maps.LatLngBounds();
					for (var i = 0; i < $scope.markers.length; i++) {
						bounds.extend(new google.maps.LatLng($scope.markers[i].position.latitude, $scope.markers[i].position.longitude));
					}
					$scope.mapinstance.fitBounds(bounds);
				}
			}
		};

		$scope.reportErrorPlaceId = "";
		$scope.reportErrorPlaceName = "";

		$scope.showReportErrorPopup = function (placeid, placename) {
			$scope.reportErrorPlaceId = placeid;
			$scope.reportErrorPlaceName = placename;
			$scope.reportErrorFormData = {
				"ua": navigator.userAgent,
				"reported_url": $scope.currentUrl,
				"place_id": $scope.reportErrorPlaceId,
				"place_name": $scope.reportErrorPlaceName
			};
			$scope.showReportError = true;
		};

		$scope.reportErrorFormData = {
			"ua": navigator.userAgent,
			"reported_url": $scope.currentUrl,
			"place_id": $scope.reportErrorPlaceId,
			"place_name": $scope.reportErrorPlaceName
		};

		$scope.sendErrorReport = function () {
			mapService.reportError($scope.reportErrorFormData);
			$scope.showReportError = false;
			$scope.popupmessage = "Thank you your issue has been submitted";
		};

		// Map and map.window settings
		$scope.map = {
			control: {},
			center: { latitude: center_lat, longitude: center_lng },
			zoom: defaultzoom,
			styles: [{ "featureType": "administrative", "elementType": "labels.text.fill", "stylers": [{ "color": "#2a3033" }] }, { "featureType": "administrative.province", "elementType": "geometry.stroke", "stylers": [{ "visibility": "on" }, { "color": "#981e32" }, { "weight": "1.26" }] }, { "featureType": "administrative.land_parcel", "elementType": "geometry.fill", "stylers": [{ "visibility": "off" }, { "color": "#b06a6a" }] }, { "featureType": "landscape.man_made", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "saturation": "-28" }, { "color": "#ffffff" }, { "weight": 2 }] }, { "featureType": "landscape.man_made", "elementType": "geometry.stroke", "stylers": [{ "visibility": "on" }, { "color": "#adadad" }, { "weight": 1 }] }, { "featureType": "landscape.man_made", "elementType": "labels.text.fill", "stylers": [{ "color": "#981e32" }] }, { "featureType": "landscape.man_made", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape.man_made", "elementType": "labels.icon", "stylers": [{ "color": "#ff0000" }, { "visibility": "off" }] }, { "featureType": "landscape.natural", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#ffffff" }] }, { "featureType": "landscape.natural.landcover", "elementType": "geometry.fill", "stylers": [{ "visibility": "off" }, { "color": "#c33737" }] }, { "featureType": "landscape.natural.terrain", "elementType": "geometry.fill", "stylers": [{ "color": "#eeeeee" }, { "visibility": "on" }] }, { "featureType": "poi.school", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#b5babe" }] }, { "featureType": "poi.school", "elementType": "geometry.stroke", "stylers": [{ "color": "#895959" }, { "visibility": "off" }] }, { "featureType": "poi.school", "elementType": "labels.text.fill", "stylers": [{ "color": "#2a3033" }] }, { "featureType": "poi.school", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "saturation": -100 }, { "lightness": 45 }] }, { "featureType": "road", "elementType": "labels.text.fill", "stylers": [{ "color": "#2a3033" }] }, { "featureType": "road", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "road.highway", "elementType": "all", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "road.highway", "elementType": "geometry.fill", "stylers": [{ "color": "#4e4e4e" }] }, { "featureType": "road.highway", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }, { "featureType": "road.arterial", "elementType": "labels.text.fill", "stylers": [{ "visibility": "on" }, { "color": "#2a3033" }] }, { "featureType": "road.arterial", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "road.local", "elementType": "labels.text.fill", "stylers": [{ "visibility": "on" }, { "color": "#2a3033" }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "color": "#d4e4eb" }, { "visibility": "on" }] }, { "featureType": "water", "elementType": "geometry.fill", "stylers": [{ "color": "#e6f0f4" }, { "visibility": "on" }] }, { "featureType": "water", "elementType": "labels.text.fill", "stylers": [{ "color": "#8f8fb1" }] }, { "featureType": "water", "elementType": "labels.text.stroke", "stylers": [{ "visibility": "off" }] }],
			events: {
				resize: function () {
				},
				tilesloaded: function () { }
			},
			markersEvents: {
				click: function (marker, eventName, model) {
					$scope.showWindow(model);
				}
			},
			options: {
				disableDefaultUI: true,
				zoomControl: true,
				mapTypeControl: true,
				mapTypeControlOptions: {
					style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
					position: google.maps.ControlPosition.TOP_RIGHT
				}
			},
			polys: [],
			window: {
				show: false,
				parent: $scope,
				closeClick: function () {
					this.show = false;
					$scope.map.window.model = 0;
					$scope.mapinstance.setOptions({ scrollwheel: true });
				},
				fieldIsntEmpty: function (field) {
					if (field && field.type) {
						var json = field.value;
						json = json.replace(/(\r\n|\n|\r)/gm, "");
						var fieldvalue = JSON.parse(json).selections[0].val;
						var fieldname = field.type.name;
						if (fieldvalue === "no" || fieldvalue === null)
						{
							return false;
						}

						return true;
					}
					return false;
				},
				isADA: function (field) {
					var json = field.value;
					json = json.replace(/(\r\n|\n|\r)/gm, "");
					var fieldvalue = JSON.parse(json).selections[0].val;
					var fieldname = field.type.name;
					return fieldname.indexOf('ADA') >= 0;
				},
				isntADA: function (field) {
					var json = field.value;
					json = json.replace(/(\r\n|\n|\r)/gm, "");
					var fieldvalue = JSON.parse(json).selections[0].val;
					var fieldname = field.type.name;
					return fieldname.indexOf('ADA') === -1;
				},
				getName: function () {
					if ($scope.map.window.model) {
						if ($scope.map.window.model.infoTitle) {
							return $scope.map.window.model.infoTitle;
						}
						if ($scope.map.window.model.prime_name) {
							return $scope.map.window.model.prime_name;
						}
					}
					return "";
				},
				getImageLink: function (imageid) {
					if ($scope.map.window.model) {
						return "/api/v1/media/" + imageid + "?placeid=" + $scope.map.window.model.id;
					}
					return "";
				},
				showDirectionsFrom: function (place) { $scope.showDirectionsFrom(place); },
				showReportErrorPopup: $scope.showReportErrorPopup,
				options: {
					alignBottom: true,
					boxClass: "infoBox",
					disableAutoPan: false,
					height: "340px",
					pixelOffset: new google.maps.Size(-200, -30),
					zIndex: 999,
					boxStyle: {
						width: 400 + "px"
					},
					infoBoxClearance: new google.maps.Size(75, 60),
					isHidden: false,
					scrollWheel: false,
					pane: "floatPane",
					enableEventPropagation: false
				}
			}
		};
		// This will hold the map instance variable once it's initialized
		$scope.mapinstance = {};
		$scope.directionService = null;
		$scope.directionsDisplay = null;
		// Round about way to get map object
		uiGmapIsReady.promise(1).then(function (instances) {
			instances.forEach(function (inst) {
				var map = inst.map;
				$scope.mapinstance = map;
				// Hack to make it redraw markers when marker list changes
				google.maps.event.addListener(map, 'zoom_changed', function () {
					setTimeout(function () {
						var cnt = map.getCenter();
						cnt.e += 0.000001;
						map.panTo(cnt);
						cnt.e -= 0.000001;
						map.panTo(cnt);
					}, 400);
				});
				$scope.directionService = new google.maps.DirectionsService();
				$scope.directionsDisplay = new google.maps.DirectionsRenderer();
				$scope.campusid = map_view.campusid;
				setupSavedCategoriesAndPlaceMap(map_view.categories, map_view.activePlace, map_view.campusid);
			});
		});

		$scope.showDirectionsFrom = function (place) {
			console.info('showdirections', place);
			var request = { origin: "Pullman, WA", destination: place.latitude + "," + place.longitude, optimizeWaypoints: true, travelMode: google.maps.TravelMode.DRIVING };

			$scope.directionService.route(request, function (response, status) {
				console.log(response);
				$scope.directionsDisplay.setDirections(response);
				$scope.directionsDisplay.setMap($scope.mapinstance);
				$scope.directions = response;
				$scope.$apply();
			});
		};

		$scope.drawDirectionsPath = function (place) {
			var decodedPath = google.maps.geometry.encoding.decodePath(place.shapes[0].encoded);
			var events = JSON.parse(place.shapes[0].style[0].style_obj).events;
			var strokecolor = events.rest.strokeColor;
			var strokeopacity = events.rest.strokeOpacity;
			var fillcolor = events.rest.fillColor;
			var fillopacity = events.rest.fillOpacity;
			var strokeweight = events.rest.strokeWeight;

			place.shapes[0].fill = { "color": fillcolor, "opacity": fillopacity };
			place.shapes[0].stroke = { "color": strokecolor, "weight": strokeweight, "opacity": strokeopacity };
			place.shapes[0].path = decodedPath;
			for (var i = 0; i < place.shapes[0].path.length; i++) {
				place.shapes[0].path[i].latitude = place.shapes[0].path[i].lat();
				place.shapes[0].path[i].longitude = place.shapes[0].path[i].lng();
			}
			place.shapes[0].id = place.id;
			place.shapes[0].editable = true;

			$scope.map.polys.push(place.shapes[0]);
		};

		$scope.getDirections = function () {
			var request = { origin: "Pullman, WA", destination: "Genesee, ID", optimizeWaypoints: true, travelMode: google.maps.TravelMode.DRIVING };
			var directionService = new google.maps.DirectionsService();
			directionService.route(request, function (response, status) { console.log(response); });
		};
		// reset map link
		$scope.resetMap = function () {
			$scope.closeListings();
			$scope.markers = [];
			$scope.closeWindow();
			$scope.clearShapes();
			$scope.clearTheMenu();
			$scope.setShortUrl();
			$scope.recenterMap();
		};

		// Clear any shapes drawn on the map, like building outlines or parking lot outlines
		$scope.clearShapes = function () {
			$scope.map.polys = [];
		};

		// Close the popup details window on the map
		$scope.closeWindow = function () {
			$scope.map.window.show = false;
			$scope.map.window.model = null;
		};

		// iScroll is needed for the listings
		$scope.resetAllScrollers = function (iscroll, timeout) {
			if (!timeout) {
				timeout = 750;
			}
			$timeout(function () {
				for (var scroller in iscroll) {
					iscroll[scroller].refresh();
					iscroll[scroller].scrollTo(0, 0);
				}
			}, timeout);
		};

		// This is the search results, the autocomplete list
		$rootScope.searchplaces = [];
		$scope.clearAutocomplete = function () {
			$rootScope.searchplaces = [];
			$scope.searchterm = "";
		};

		// Watch the input
		$scope.$watch("searchterm", function (newValue, oldValue, scope) {
			if (newValue && newValue.length > 2) {
				$scope.getLocation(newValue);
			}
			else {
				$rootScope.searchplaces = [];
			}
		}, true);

		// Called by code watching the search input
		$scope.getLocation = function (val) {
			var searchInputOffsetFromTop = $("#searchterm").height() + $("#searchterm").offset().top;
			// Adjust autocomplete lcoation
			$(".ui-autocomplete").css("top", searchInputOffsetFromTop + "px");
			mapService.keyWordAutoComplete(val).then(
			function (data) {
				console.info("searchplaces", data);
				sortResults(data, "label", true);
				$rootScope.searchplaces = data;
			});
		};

		/* HELPER FUNCTIONS */
		function setupInfoWindow() {
			jQuery('#popup').tabs();
			var items = jQuery('.cWrap .items');
			items.each(function () {
				jQuery('.cWrap .items').cycle({
					activePagerClass: "cycle-pager-active",
					fx: 'scrollHorz',
					delay: -2000,
					pauseOnHover: 1,
					pause: 1,
					timeout: 0,
					pager: jQuery(".cycleArea").find('.cNav'),
					pagerAnchorBuilder: function (idx, slide) {
						return '<li><a  hidefocus="true">' + idx + '</a></li>';
					},
					prev: '.prev',
					next: '.next',
					slides: '> a'
				});
			});
		}

		function sortResults(json, prop, asc) {
			json = json.sort(function (a, b) {
				if (asc) {
					return a[prop] > b[prop] ? 1 : a[prop] < b[prop] ? -1 : 0;
				}
				else {
					return b[prop] > a[prop] ? 1 : b[prop] < a[prop] ? -1 : 0;
				}
			});
			return json;
		}


		var gpsconfig = {
			enableHighAccuracy: true,
			timeout: 60 * 1000,
			maximumAge: 10 * 1000
		};

		function updateGPSLocationOnce() {
			navigator.geolocation.getCurrentPosition(onPositionUpdate, onPositionError, gpsconfig);
		}

		updateGPSLocationOnce();

		$scope.usermarkers = [];
		$scope.usermarker = { id: "myposition", position: { latitude: null, longitude: null } };
		$scope.usermarker.position.latitude = center_lat;
		$scope.usermarker.position.longitude = center_lng;
		$scope.usermarker.id = "myposition";
		$scope.usermarker.markeroptions = {
			optimized: false,
			icon: {
				url: "/Content/images/ico-marker-current.png",
				size: new google.maps.Size(80, 80),
				scaledSize: new google.maps.Size(40, 40),
				// The origin for this image is 0,0.
				origin: new google.maps.Point(0, 0),
				// The anchor for this image is the base of the flagpole at 0,32.
				anchor: new google.maps.Point(20, 20)
			}
		};
		$scope.usermarkers.push($scope.usermarker);

		function onPositionUpdate(position) {
			$scope.usermarker.position.latitude = position.coords.latitude;
			$scope.usermarker.position.longitude = position.coords.longitude;
		}

		function onPositionError(error) {
			console.log(error);
		}

		// Called after google map is ready
		// Open category and clicked place from small url
		function setupSavedCategoriesAndPlaceMap(categoryids, placeid, campusid) {
			$scope.campusid = campusid;
			// Menu setup -- We dynamically get the JSON feed to make the menu
			mapService.getCategoryList().then(function (data) {
				$scope.categories = data;
				// Setup saved place
				if (categoryids) {
					var loadcats = categoryids.split(',');
					for (var j = 0; j < loadcats.length; j++) {
						// Setup saved category
						var clickedcat = $scope.getCategoryByName(loadcats[j]).active = true;
					}
				}
				$scope.toggleActiveMenuItem().then(function () {
					if (placeid !== null) {
						for (var i = 0; i < $scope.markers.length; i++) {
							if ($scope.markers[i].id === parseInt(placeid)) {
								$scope.showWindow($scope.markers[i]);
							}
						}
					}
				});
			});
		}
	});
}());