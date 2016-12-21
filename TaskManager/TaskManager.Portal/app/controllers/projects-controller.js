﻿; (function () {
    'use strict';

    angular
        .module('app')
        .controller('ProjectsController', ProjectsController);

    function ProjectsController($scope, ngDialog, ProjectService) {
        (function init() {
            ProjectService.getProjects().then(
                function (response) {
                    var data = response;
                    $scope.projects = data;
                },
                function (error) {

                });
        })();

        $scope.newProject = function () {
            var newDialog = ngDialog.open({
                template: '/app/views/project.html',
                controller: 'ProjectController',
                scope: $scope,
                data: {
                    type: 'new'
                }
            });

            newDialog.closePromise.then(function (data) {
                if (data.value && data.value.Id) {
                    $scope.projects.push(data.value)
                }
            });
        };

        $scope.viewProject = function (project) {
            var viewDialog = ngDialog.open({
                template: '/app/views/project.html',
                controller: 'ProjectController',
                scope: $scope,
                data: {
                    project: project,
                    type: 'view'
                }
            });
        };

        $scope.editProject = function (project) {
            var editDialog = ngDialog.open({
                template: '/app/views/project.html',
                controller: 'ProjectController',
                scope: $scope,
                data: {
                    project: project,
                    type: 'edit'
                }
            });

            editDialog.closePromise.then(function (data) {
                if (data.value && data.value.Id) {
                    var index = -1;
                    $scope.projects.forEach(function (item, i) {
                        if (item.Id == data.value.Id) {
                            index = i;
                        }
                    });
                    $scope.projects[index] = data.value;
                }
            });
        };

        $scope.removeProject = function (project) {
            ProjectService.removeProject()
                .then(function (response) {

                },
                function (error) {
                });
        };
    };
})();