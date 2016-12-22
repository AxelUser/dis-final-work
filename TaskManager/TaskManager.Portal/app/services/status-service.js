; (function () {
    'use strict';

    angular
        .module('app')
        .service('StatusService', StatusService);

    function StatusService($http, $routeParams, $rootScope, $q) {
        this.getStatuses = function (id) {
            return $http.get('/TaskStatusTypes/List');
        };
    };
})();