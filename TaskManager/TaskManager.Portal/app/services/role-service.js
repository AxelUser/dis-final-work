; (function () {
    'use strict';

    angular
        .module('app')
        .service('RoleService', RoleService);

    function RoleService($http, $routeParams, $rootScope, $q) {
        this.getRoles = function (id) {
            return $q(function (resolve, reject) {
                resolve([
                    {
                        Id: 1,
                        Caption: 'Caption 1'
                    },
                    {
                        Id: 2,
                        Caption: 'Caption 2'
                    },
                    {
                        Id: 3,
                        Caption: 'Caption 3'
                    },
                    {
                        Id: 4,
                        Caption: 'Caption 4'
                    },
                ]);
            });
            return $http.get('/Task/List' + id);
        };
    };
})();