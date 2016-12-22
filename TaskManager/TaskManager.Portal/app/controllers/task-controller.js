; (function () {
    'use strict';

    angular
        .module('app')
        .controller('TaskController', TaskController);

    function TaskController($scope, ngDialog, TaskService, StatusService) {
        (function init() {
            $scope.entityTask = $scope.ngDialogData.task ? angular.copy($scope.ngDialogData.task) : {};
            $scope.controllerType = $scope.ngDialogData.type;

            switch ($scope.controllerType) {
                case 'view': $scope.formTitle = 'Просмотр задачи';
                case 'edit': $scope.formTitle = 'редактирование задачи';
                case 'new': $scope.formTitle = 'Создание задачи';
            }

            StatusService.getStatuses()
                .then(function (response) {
                    var data = response.data;
                    $scope.statuses = data;
                },
                function (error) {

                });

        })();

        $scope.saveTask = function () {
            TaskService.saveTask($scope.entityTask).then(
                function (response) {
                    $scope.closeThisDialog(response.data);
                },
                function (error) {

                });
        };

        $scope.newExecutor = function () {
            var newDialog = ngDialog.open({
                template: '/app/views/executor.html',
                controller: 'ExecutorController',
                scope: $scope,
                data: {
                    type: 'new'
                }
            });

            newDialog.closePromise.then(function (data) {
                if (data.value) {
                    $scope.entityTask.ExecutorRoles.push(data.value);
                }
            });
        };

        $scope.editExecutor = function (executor) {
            var editDialog = ngDialog.open({
                template: '/app/views/executor.html',
                controller: 'ExecutorController',
                scope: $scope,
                data: {
                    executor: executor,
                    type: 'edit'
                }
            });

            editDialog.closePromise.then(function (data) {
                if (data.value && data.value.Id) {
                    var index = -1;
                    $scope.entityTask.ExecutorRoles.forEach(function (item, i) {
                        if (item.Id == data.value.Id) {
                            index = i;
                        }
                    });
                    $scope.entityTask.ExecutorRoles[index] = data.value;
                }
            });
        };
    };
})();