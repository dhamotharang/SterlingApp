(function () {
    "use strict";
    angular
        .module("smarterp.extra")
        .controller("AccountChangePasswordCtrl",
                    [AccountLoginCtrl]);

    function AccountLoginCtrl($http, $location) {

        var vm = this;

        vm.passwordModel = new SmartERP.AccountChangePasswordModel();
        vm.viewMode = 'changepw'; // changepw, success

        var initialize = function () {

            if (vm.init === false) {

                initializeView();
                vm.init === true;
            }
        }

        var initializeView = function () {

        }

        vm.changePassword = function () {

        }

        initialize();
    }
}());

