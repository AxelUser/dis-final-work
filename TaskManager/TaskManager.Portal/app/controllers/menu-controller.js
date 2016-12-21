; (function () {
    'use strict';

    angular
        .module('app')
        .controller('MenuController', MenuController);

    function MenuController($scope, $location) {
        $scope.isAuthenticated = function () {
            return false;
        };
    };
})();