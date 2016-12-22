; (function () {
    'use strict';

    angular
        .module('app')
        .service('UserService', UserService);

    function UserService($http, $routeParams, $rootScope, $q) {
        this.getUsers = function () {
            return $http.get('/Users/Index');
        };

        this.getProject = function () {
            return $http.get('/Users/Details' + $routeParams.id);
        }
    };
})();