; (function () {
    angular.module('app')
        .directive("scroll", function ($window, $location) {
            return function (scope, element, attrs) {
                angular.element($window).bind("scroll", function () {
                    if (this.pageYOffset >= 100) {
                        scope.boolChangeClass = true;
                    } else {
                        scope.boolChangeClass = false && ($location.hash() === '');
                    }
                    scope.$apply();
                });
                if ($location.hash() !== '') {
                    scope.boolChangeClass = true;
                }
            };
        });
})();