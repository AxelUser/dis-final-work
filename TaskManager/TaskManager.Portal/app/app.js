; (function () {
    angular
        .module('app', ['ngRoute', 'ngSanitize']);
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
                    .when('/tasks', {
                        title: 'Задачи проекта',
                        templateUrl: 'app/views/tasks.html',
                        controller: 'TasksController'
                    })
                    .otherwise('/');
            }])
        .run(['$rootScope', function ($rootScope) {
            $rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
                $rootScope.$pageTitle = current.$$route.title;
            });
        }]);
})();