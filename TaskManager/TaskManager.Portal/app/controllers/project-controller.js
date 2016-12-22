; (function () {
    'use strict';

    angular
        .module('app')
        .controller('ProjectController', ProjectController);

    function ProjectController($scope, ngDialog, ProjectService, UserService, TaskService) {
        (function init() {
            $scope.entity = $scope.ngDialogData.project ? angular.copy($scope.ngDialogData.project) : {};
            $scope.controllerType = $scope.ngDialogData.type;

            switch ($scope.controllerType) {
                case 'view': $scope.formTitle = 'Просмотр проекта';
                case 'edit': $scope.formTitle = 'Редактирование проекта';
                case 'new': $scope.formTitle = 'Создание проекта';
            }

            UserService.getUsers()
                .then(function (response) {
                    var data = response.data;
                    $scope.users = data;
                },
                function (error) {

                });
            if ($scope.entity.Id) {
                TaskService.getTasks($scope.entity.Id).then(
                   function (response) {
                       var data = response.data;
                       $scope.entity.Tasks = data;
                   },
                   function (error) {

                   });
            } else {
                $scope.entity.Tasks = [];
            }
        })();

        $scope.saveEntity = function () {
            ProjectService.saveProject($scope.entity)
                .then(function (response) {
                    $scope.closeThisDialog(response.data);
                },
                function (error) {

                });
        };

        $scope.newTask = function () {
            var newDialog = ngDialog.open({
                template: '/app/views/task.html',
                controller: 'TaskController',
                scope: $scope,
                data: {
                    type: 'new'
                }
            });

            newDialog.closePromise.then(function (data) {
                if (data.value) {
                    $scope.entity.Tasks.push(data.value);
                }
            });
        };

        $scope.editTask = function (task) {
            var editDialog = ngDialog.open({
                template: '/app/views/task.html',
                controller: 'TaskController',
                scope: $scope,
                data: {
                    task: task,
                    type: 'edit'
                }
            });

            editDialog.closePromise.then(function (data) {
                if (data.value && data.value.Id) {
                    var index = -1;
                    $scope.entity.Tasks.forEach(function (item, i) {
                        if (item.Id == data.value.Id) {
                            index = i;
                        }
                    });
                    $scope.entity.Tasks[index] = data.value;
                }
            });
        };

        $scope.doExport = function () {
            ProjectService.doExport($scope.entity.Id)
                .then(function (response) {

                },
                function (error) {

                });
        }

        $scope.removeProject = function (task) {
            TaskService.removeTask(task.Id)
                .then(function (response) {
                    var index = -1;
                    $scope.entity.Tasks.forEach(function (item, i) {
                        if (item.Id == data.value.Id) {
                            index = i;
                        }
                    });
                    $scope.entity.Tasks.splice(index, 1);
                },
                function (error) {

                });
        };
    };
})();