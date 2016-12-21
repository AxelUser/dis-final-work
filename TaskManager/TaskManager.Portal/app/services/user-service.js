; (function () {
    'use strict';

    angular
        .module('app')
        .service('UserService', UserService);

    function UserService($http, $routeParams, $rootScope, $q) {
        this.getUsers = function () {
            return $q(function (resolve, reject) {
                resolve([
                    {
                        Id: 123,
                        Login: 'Login',
                        FullName: 'FullName1',
                        Email: 'Email'
                    },
                    {
                        Id: 124,
                        Login: 'Login',
                        FullName: 'FullName2',
                        Email: 'Email'
                    },
                    {
                        Id: 115,
                        Login: 'Login',
                        FullName: 'FullName3',
                        Email: 'Email'
                    },
                    {
                        Id: 2,
                        FullName: 'Full Name 1'
                    }
                ]);
            });
            return $http.get('/Project/Index');
        };

        this.getProject = function () {
            return $http.get('/Project/' + $routeParams.id);
        }
    };
})();