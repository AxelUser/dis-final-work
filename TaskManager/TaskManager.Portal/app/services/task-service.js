; (function () {
    'use strict';

    angular
        .module('app')
        .service('TaskService', TaskService);

    function TaskService($http, $routeParams, $rootScope, $q) {
        this.getTasks = function (id) {
            return $http.get('/ProjectTasks/Index?id=' + id);
        };

        this.getTask = function (id) {
            return $http.get('/Task/' + id);
        };

        this.saveTask = function (data) {
            if (data.Id) {
                data.UpdateDate = new Date();
                var d = new Date();
                d.setTime(data.CreationDate.substr(6, 13));
                data.CreationDate = d.toISOString();
                return $http.post('/ProjectTasks/Edit/', data);
            } else {
                data.UpdateDate = new Date();
                data.CreationDate = new Date();
                return $http.post('/ProjectTasks/Create/', data);
            }
        };

        this.removeTask = function (id) {
            return $http.post('/ProjectTasks/Delete', { id: id });
        }

        this.getExecutors = function (id) {
            return $http.get('/ExecutorRoles/Index?id=' + id);
        }
    };
})();