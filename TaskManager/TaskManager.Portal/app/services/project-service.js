; (function () {
    'use strict';

    angular
        .module('app')
        .service('ProjectService', ProjectService);

    function ProjectService($http, $routeParams, $rootScope, $q) {
        this.getProjects = function () {
            return $http.get('/Projects/Index');
        };

        this.getProject = function (id) {
            return $http.get('/Projects/Details?id=' + id);
        }

        this.removeProject = function (id) {
            return $http.post('/Projects/Delete', { id: id });
        }

        this.saveProject = function (data) {
            if (data.Id) {
                return $http.post('/Projects/Edit', data);
            } else {
                return $http.post('/Projects/Create', data);
            }
        }

        this.doExport = function (id) {
            return $http.get('/Projects/Export?id=' + id)
        }
    };
})();