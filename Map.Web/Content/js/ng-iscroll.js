/*!
Copyright (c) 2013 Brad Vernon <bradbury.vernon@gmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/
var myScroll;
document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
angular.module('ng-iscroll', []).directive('ngIscroll', function ($rootScope)
{
    return {
        replace: false,
        restrict: 'A',
        link: function (scope, element, attr)
        {
            // default timeout
            var ngiScroll_timeout = 5;


            // default options
            var ngiScroll_opts = {
			hScroll: false,
			preventDefault:  false, //!isAndroid(),
			click: false,
			tap: false,
			mouseWheel:true
			
			/*eventPassthrough: true*/
				
            };

            // scroll key /id
            var scroll_key = attr.ngIscroll;

            if (scroll_key === '') {
                scroll_key = attr.id;
            }
			//logInfo("setting up "+scroll_key);
            // if ng-iscroll-form='true' then the additional settings will be supported
            if (attr.ngIscrollForm !== undefined && attr.ngIscrollForm == 'true') {
                ngiScroll_opts.useTransform = false;
                ngiScroll_opts.onBeforeScrollStart = function (e)
                {
                    var target = e.target;
                    while (target.nodeType != 1) target = target.parentNode;

                    if (target.tagName != 'SELECT' && target.tagName != 'INPUT' && target.tagName != 'TEXTAREA')
                        e.preventDefault();
                };
            }
			var thescope = scope;
				
			if(scope.$parent)
				thescope = scope.$parent;
            if (thescope.myScrollOptions) {
                for (var i in thescope.myScrollOptions) {
					if (i === scroll_key) {
                        for (var k in thescope.myScrollOptions[i]) {
                            ngiScroll_opts[k] = thescope.myScrollOptions[i][k];
                        }
                    } else {
						try{ ngiScroll_opts[i] = scope.$root.myScrollOptions[i]; } catch(e){}
                    }
                }
            }

            // iScroll initialize function
            function setScroll()
            {
				var thescope = scope;
				
				if(scope.$parent)
					thescope = scope.$parent;
				
                if (thescope.myScroll === undefined) {
                    thescope.myScroll = [];
                }
                thescope.myScroll[scroll_key] = new IScroll(element[0], ngiScroll_opts);
				
				thescope.myScroll[scroll_key].on("beforeScrollStart", function(e) {
					//console.log(e);
					/*var target = e.target;
                    while (target.nodeType != 1) target = target.parentNode;

                    if (target.tagName != 'SELECT' && target.tagName != 'INPUT' && target.tagName != 'TEXTAREA')
                        e.preventDefault();*/
				});
				
				
				// add ng-iscroll-refresher for watching dynamic content inside iscroll
				if(attr.ngIscrollRefresher !== undefined) {
					scope.$watch(attr.ngIscrollRefresher, function ()
					{	
						thescope.myScroll[scroll_key].refresh();
						if(thescope.myScroll[scroll_key] !== undefined) 
						{
							setTimeout(function(){  if(thescope){ thescope.myScroll[scroll_key].refresh(); }  }, 450);
						}
					});
				}
            }

            // new specific setting for setting timeout using: ng-iscroll-timeout='{val}'
            if (attr.ngIscrollDelay !== undefined) {
                ngiScroll_timeout = attr.ngIscrollDelay;
            }

            // watch for 'ng-iscroll' directive in html code
            scope.$watch(attr.ngIscroll, function ()
            {
				setTimeout(setScroll, 500);
            });

			
        }
    };
});
