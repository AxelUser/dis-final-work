; (function () {
    'use strict';

    angular
        .module('app')
        .controller('ProjectController', ProjectController);

    function ProjectController($scope, ngDialog, ProjectService, UserService, TaskService, ExceptionService) {
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
                    ExceptionService.alert();
                });
            $scope.entity.ProjectTasks = [];
            if ($scope.entity.Id) {
                TaskService.getTasks($scope.entity.Id).then(
                   function (response) {
                       var data = response.data;
                       data.forEach(function (item) {
                           var d1 = new Date();
                           d1.setTime(item.CreationDate.substr(6, 13));
                           var d2 = new Date();
                           (d2.setTime(item.UpdateDate.substr(6, 13)));
                           item.CreationDateView = d1.toLocaleDateString();
                           item.UpdateDateView = d2.toLocaleDateString();
                       })
                       $scope.entity.ProjectTasks = data;
                   },
                   function (error) {
                       ExceptionService.alert();
                   });
            }
        })();

        $scope.saveEntity = function () {
            ProjectService.saveProject($scope.entity)
                .then(function (response) {
                    $scope.closeThisDialog(response.data);
                },
                function (error) {
                    ExceptionService.alert();
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
                if (data.value && data.value.Id) {
                    var d1 = new Date();
                    d1.setTime(data.value.CreationDate.substr(6, 13));
                    var d2 = new Date();
                    (d2.setTime(data.value.UpdateDate.substr(6, 13)));
                    data.value.CreationDateView = d1.toLocaleDateString();
                    data.value.UpdateDateView = d2.toLocaleDateString();
                    $scope.entity.ProjectTasks.push(data.value);
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
                    var d1 = new Date();
                    d1.setTime(data.value.CreationDate.substr(6, 13));
                    var d2 = new Date();
                    (d2.setTime(data.value.UpdateDate.substr(6, 13)));
                    data.value.CreationDateView = d1.toLocaleDateString();
                    data.value.UpdateDateView = d2.toLocaleDateString();
                    $scope.entity.ProjectTasks.forEach(function (item, i) {
                        if (item.Id == data.value.Id) {
                            index = i;
                        }
                    });
                    $scope.entity.ProjectTasks[index] = data.value;
                }
            });
        };

        $scope.doExport = function () {
            ProjectService.doExport($scope.entity.Id)
                .then(function (response) {

                },
                function (error) {
                    ExceptionService.alert();
                });
        }

        $scope.removeTask = function (task) {
            TaskService.removeTask(task.Id)
                .then(function (response) {
                    var index = -1;
                    $scope.entity.ProjectTasks.forEach(function (item, i) {
                        if (item.Id == task.Id) {
                            index = i;
                        }
                    });
                    $scope.entity.ProjectTasks.splice(index, 1);
                },
                function (error) {
                    ExceptionService.alert();
                });
        };
    };
})();