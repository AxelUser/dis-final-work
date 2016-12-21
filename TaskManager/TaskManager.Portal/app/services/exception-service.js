; (function () {
    'use strict';

    angular
        .module('app')
        .factory('ExceptionService', ExceptionService);

    function ExceptionService(ngDialog) {
        var util = {};

        util.alert = function (title, text) {
            ngDialog.open({
                template: '<div><div class="row"><h3>{{ngDialogData.title}}</h3></div><div><p>{{ngDialogData.text}}</p></div></div>',
                plain: true,
                data: { title: title, text: text }
            });
        }

        return util;
    };
})();