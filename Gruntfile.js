module.exports = function(grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        stylelint: {
            src: [ "Map.Web/Content/css/main.css" ]
        },

        less: {
            options: {
                strictMath: true,
				sourceMap:true,
				outputSourceFiles: false,
				plugins : [ new (require('less-plugin-autoprefix'))({cascade:false, browsers : [ "> 1%", "ie 8-11", "Firefox ESR"  ]}) ]
            },
            development: {
                files: {
                    'Map.Web/Content/css/main.css': 'Map.Web/Content/css/main.less'
                }
            }
        },
		
		lesshint: {			
			useLesshintRc: {
                options: {
                    lesshintrc : '.lesshintrc'
                },
                files: {
                    src: [ 'Map.Web/Content/css/main.less' ]
                }
            }
		},

        watch: {
            styles: {
                files: [ "Map.Web/Content/css/*.less" ],
                tasks: [ "default" ],
                option: {
                    livereload: 8000
                }
            }
        },

		sed: {
			tabs: {
				path: "Map.Web/Content/css/main.css",
				pattern: "  ",
				replacement: '\t',
				recursive: false
			},
			zeros: {
				path: "Map.Web/Content/css/main.css",
				pattern: '0[.]',
				replacement: '.',
				recursive: false
			},
			squiggleys: {
				path: "Map.Web/Content/css/main.css",
				pattern: '}',
				replacement: '}\r\n',
				recursive: false
			},
			tabspace: {
				path: "Map.Web/Content/css/main.css",
				pattern: '\t ',
				replacement: '\t',
				recursive: false
			},
			nestedspace: {
				path: "Map.Web/Content/css/main.css",
				pattern: '[)] \{',
				replacement: ') {\r\n',
				recursive: false
			},
			addspaceatend: {
				path: "Map.Web/Content/css/main.css",
				pattern: ' [*]/',
				replacement: ' */\n',
				recursive: false
			},
			squiggleysextraline: {
				path: "Map.Web/Content/css/main.css",
				pattern: '(@(-webkit-|)keyframes.+{)',
				replacement: '$1\r\n',
				recursive: true
			}
		},
 
		uglify: {
			min: {
			  files: {
				'Map.Web/Content/js/vendor/output.min.js': [	"bower_components/jquery/dist/jquery.min.js",
																"bower_components/jquery-ui/jquery-ui.min.js",
																"bower_components/iscroll/build/iscroll.js",
																"bower_components/jquery-cycle/jquery.cycle.all.js",
																"bower_components/angular/angular.js",
																"bower_components/angular-iscroll/dist/lib/angular-iscroll.js",
																"bower_components/angular-route/angular-route.js",
																"bower_components/angular-ui-router/release/angular-ui-router.min.js",
																"bower_components/angular-animate/angular-animate.js",
																"bower_components/angular-touch/angular-touch.js",
																"bower_components/angular-sanitize/angular-sanitize.js",
																"bower_components/angular-click-outside/clickoutside.directive.js",
																"bower_components/angular-local-storage/dist/angular-local-storage.min.js",
																"bower_components/angular-once/once.js",
																"bower_components/lodash/dist/lodash.min.js",
																"bower_components/angular-simple-logger/dist/angular-simple-logger.min.js",
																"bower_components/angular-google-maps/dist/angular-google-maps.js"]
			  },
			  options: {
				beautify: false,
				compress: true
			  }
			},
			
			pretty: {
				files: {
					'Map.Web/Content/js/vendor/output.js': [	"bower_components/jquery/dist/jquery.min.js",
																"bower_components/jquery-ui/jquery-ui.min.js",
																"bower_components/iscroll/build/iscroll.js",
																"bower_components/jquery-cycle/jquery.cycle.all.js",
																"bower_components/angular/angular.js",
																"bower_components/angular-iscroll/dist/lib/angular-iscroll.js",
																"bower_components/angular-route/angular-route.js",
																"bower_components/angular-ui-router/release/angular-ui-router.min.js",
																"bower_components/angular-animate/angular-animate.js",
																"bower_components/angular-touch/angular-touch.js",
																"bower_components/angular-sanitize/angular-sanitize.js",
																"bower_components/angular-click-outside/clickoutside.directive.js",
																"bower_components/angular-local-storage/dist/angular-local-storage.min.js",
																"bower_components/angular-once/once.js",
																"bower_components/lodash/dist/lodash.min.js",
																"bower_components/angular-simple-logger/dist/angular-simple-logger.min.js",
																"bower_components/angular-google-maps/dist/angular-google-maps.js"]
				},
				options: {
				beautify: true,
				compress: false
				}
			}						
		}
    });


	
	
	grunt.loadNpmTasks( "grunt-contrib-clean" );
	grunt.loadNpmTasks( "grunt-contrib-copy" );
	grunt.loadNpmTasks( "grunt-contrib-jshint" ); 
    grunt.loadNpmTasks( "grunt-contrib-less" );
	grunt.loadNpmTasks( "grunt-contrib-uglify" );
    grunt.loadNpmTasks( "grunt-contrib-watch" );
	grunt.loadNpmTasks( 'grunt-lesshint' );
	grunt.loadNpmTasks( "grunt-sed" );
    grunt.loadNpmTasks( "grunt-stylelint" );
	

    // Default task(s).
    grunt.registerTask( "default", [ "lesshint", "less", "sed", "stylelint", "uglify" ] );
    grunt.registerTask( "serve", [ "connect", "watch" ] );
};
