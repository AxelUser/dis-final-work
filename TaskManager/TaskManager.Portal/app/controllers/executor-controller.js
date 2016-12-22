; (function () {
    'use strict';

    angular
        .module('app')
        .controller('ExecutorController', ExecutorController);

    function ExecutorController($scope, TaskService, RoleService, UserService) {
        (function init() {
            $scope.entityExecutor = $scope.ngDialogData.executor ? angular.copy($scope.ngDialogData.executor) : {};
            $scope.controllerType = $scope.ngDialogData.type;

            switch ($scope.controllerType) {
                case 'view': $scope.formTitle = 'Просмотр исполнителя';
                case 'edit': $scope.formTitle = 'Редактирование исполнителя';
                case 'new': $scope.formTitle = 'Создание исполнителя';
            }

            UserService.getUsers()
                .then(function (response) {
                    var data = response.data;
                    $scope.users = data;
                },
                function (error) {

                });

            RoleService.getRoles()
                .then(function (response) {
                    var data = response.data;
                    $scope.roles = data;
                },
                function (error) {

                });
        })();
    };
})();