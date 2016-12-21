; (function () {
    'use strict';

    angular
        .module('app')
        .service('ProjectService', ProjectService);

    function ProjectService($http, $routeParams, $rootScope, $q) {
        this.getProjects = function () {
            return $q(function (resolve, reject) {
                resolve([
                    {
                        Id: 1,
                        Title: 'Title 1',
                        User: {
                            Id: 123,
                            Login: 'Login',
                            FullName: 'FullName1',
                            Email: 'Email'
                        }
                    },
                    {
                        Id: 2,
                        Title: 'Title 2',
                        User: {
                            Id: 123,
                            Login: 'Login',
                            FullName: 'FullName1',
                            Email: 'Email'
                        }
                    },
                    {
                        Id: 3,
                        Title: 'Title 3',
                        User: {
                            Id: 123,
                            Login: 'Login',
                            FullName: 'FullName1',
                            Email: 'Email'
                        }
                    }
                ]);
            });
            return $http.get('/Projects/Index');
        };

        this.getProject = function (id) {
            return $http.get('/Projects/' + id);
        }

        this.removeProject = function (id) {
            return $http.delete('/Projects/' + id);
        }

        this.saveProject = function (data) {
            return $http.post('/Projects', data);
        }

        this.doExport = function (id) {
            $http.get('/Projects/Export' + id)
        }
    };
})();