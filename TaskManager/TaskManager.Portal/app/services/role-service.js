; (function () {
    'use strict';

    angular
        .module('app')
        .service('RoleService', RoleService);

    function RoleService($http, $routeParams, $rootScope, $q) {
        this.getRoles = function () {
            return $http.get('/ExecutorRoles/Index/');
        };
    };
})();