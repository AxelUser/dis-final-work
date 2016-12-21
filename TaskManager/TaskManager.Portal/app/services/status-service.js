; (function () {
    'use strict';

    angular
        .module('app')
        .service('StatusService', StatusService);

    function StatusService($http, $routeParams, $rootScope, $q) {
        this.getStatuses = function (id) {
            return $q(function (resolve, reject) {
                resolve([
                    {
                        Id: 1,
                        Caption: 'Status 1'
                    },
                    {
                        Id: 2,
                        Caption: 'Status 2'
                    },
                    {
                        Id: 3,
                        Caption: 'Status 3'
                    },
                    {
                        Id: 4,
                        Caption: 'Status 4'
                    },
                ]);
            });
            return $http.get('/Task/List' + id);
        };
    };
})();