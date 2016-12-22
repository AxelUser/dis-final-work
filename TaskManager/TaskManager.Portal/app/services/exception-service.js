; (function () {
    'use strict';

    angular
        .module('app')
        .factory('ExceptionService', ExceptionService);

    function ExceptionService(ngDialog) {
        var util = {};

        util.alert = function (title, text) {
            if (!title || !text)
                alertDefault()
            else
                ngDialog.open({
                    template: '<div><div class="row"><h3>{{ngDialogData.title}}</h3></div><div><p>{{ngDialogData.text}}</p></div></div>',
                    plain: true,
                    data: { title: title, text: text }
                });
        }

        util.alertDefault = function (title, text) {
            ngDialog.open({
                template: '<div><div class="row"><h3 class="color-red">Ошибка</h3></div><div><p class="color-red">Возникла непредвиденная ошибка. <br> Попробуйте повторить операцию позднее</p></div></div>',
                plain: true,
                data: { title: title, text: text }
            });
        }

        return util;
    };
})();