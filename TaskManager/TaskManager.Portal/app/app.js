; (function () {
    angular
        .module('app', ['ngRoute', 'ngSanitize', 'ngDialog',
            
        ]);
    angular.module('app')
        .config(['$locationProvider', '$routeProvider',
            function config($locationProvider, $routeProvider) {
                $routeProvider
                    .when('/', {
                        title: 'Главная страница',
                        templateUrl: 'app/views/main.html',
                        controller: 'MainController'
                    })
                    .when('/projects', {
                        title: 'Проекты',
                        templateUrl: 'app/views/projects.html',
                        controller: 'ProjectsController'
                    })
                    .when('/task/:id', {
                        title: 'Проекты',
                        templateUrl: 'app/views/projects.html',
                        controller: 'ProjectsController'
                    })
                    .otherwise('/');
            }])
        .config(['ngDialogProvider', function (ngDialogProvider) {
            ngDialogProvider.setDefaults({
                className: 'ngdialog-custom-theme',
                showClose: true,
                closeByDocument: true,
                closeByEscape: true
            });
        }])
        .run(['$rootScope', function ($rootScope) {
            $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
                $rootScope.$pageTitle = current.$$route.title;
            });
        }]);
})();