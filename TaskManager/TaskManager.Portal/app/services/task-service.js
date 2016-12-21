; (function () {
    'use strict';

    angular
        .module('app')
        .service('TaskService', TaskService);

    function TaskService($http, $routeParams, $rootScope, $q) {
        this.getTasks = function (id) {
            return $q(function (resolve, reject) {
                resolve([
                    {
                        Id: 1,
                        Title: 'Title 1',
                        EstimatedDifficult: 10,
                        CreationDate: '10.10.2001',
                        UpdateDate: '10.10.2001',
                        TaskStatusType: {
                            Id:1,
                            Caption: 'Status 1'
                        },
                        Executors: [{
                            Id: 1,
                            RoleType: {
                                Id: 1,
                                Caption: 'Role 1'
                            },
                            User: {
                                Id: 1,
                                FullName: 'Full Name 1'
                            }
                        },
                        {
                            Id: 2,
                            RoleType: {
                                Id: 1,
                                Caption: 'Role 1'
                            },
                            User: {
                                Id: 1,
                                FullName: 'Full Name 1'
                            }
                        }]
                    },
                    {
                        Id: 2,
                        Title: 'Title 1',
                        EstimatedDifficult: 10,
                        CreationDate: '10.10.2001',
                        UpdateDate: '10.10.2001',
                        TaskStatusType: {
                            Id: 2,
                            Caption: 'Status 1'
                        },
                        Executors: [{
                            Id: 1,
                            RoleType: {
                                Id: 2,
                                Caption: 'Role 1'
                            },
                            User: {
                                Id: 2,
                                FullName: 'Full Name 1'
                            }
                        }]
                    },
                    {
                        Id: 3,
                        Title: 'Title 1',
                        EstimatedDifficult: 10,
                        CreationDate: '10.10.2001',
                        UpdateDate: '10.10.2001',
                        TaskStatusType: {
                            Id: 3,
                            Caption: 'Status 1'
                        },
                        Executors: [{
                            Id: 1,
                            RoleType: {
                                Id: 3,
                                Caption: 'Role 1'
                            },
                            User: {
                                Id: 3,
                                FullName: 'Full Name 1'
                            }
                        }]
                    }
                ]);
            });
            return $http.get('/Task/List' + id);
        };

        this.getTask = function (id) {
            return $http.get('/Task/' + id);
        }
    };
})();