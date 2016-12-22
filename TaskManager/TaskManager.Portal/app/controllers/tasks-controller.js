; (function () {
    'use strict';

    angular
        .module('app')
        .controller('TasksController', TasksController);

    function TasksController($scope, TaskService) {
        (function init() {
            TaskService.getTasks().then(
                function (response) {
                    var data = response.data;
                    $scope.tasks = data;
                },
                function (error) {

                });
        })();
    };
})();