
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AccountChangePasswordController",
                    ['$scope', '$http', '$window', '$state', '$stateParams', 'viewModelHelper', '$location', 'validator',
                        AccountChangePasswordController]);
    function AccountChangePasswordController($scope, $http, $window, $state, $stateParams, viewModelHelper, $location, validator) {


        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'changepassword-edit-view';
        vm.viewName = 'Change Password';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.changePassword = {};
        vm.oldPassword = [];
        vm.newPassword = [];
        vm.confirmPassword = [];
        vm.showRoles = true;
        var message = '';
        $scope.customStyle = {};
        vm.strength = ''
        vm.status = true;
        $scope.disabled = true;

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var changePasswordRules = [];

        var setupRules = function () {

            changePasswordRules.push(new validator.PropertyRule("OldPassword", {
                required: { message: "OldPassword is required" }
            }));

            changePasswordRules.push(new validator.PropertyRule("NewPassword", {
                required: { message: "NewPassword is required" }
            }));

            changePasswordRules.push(new validator.PropertyRule("ConfirmedPassword", {
                required: { message: "ConfirmedPassword is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {

                vm.viewModelHelper.apiGet('', null,
                function (result) {
                    vm.changePassword = result.data;
                    if (vm.changePassword === 'null')
                        initialView();
                    vm.init === true;

                },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);

            }
        }
        
        vm.changePasswordModel = function (opass, newp, comp) {


            validate(opass, newp, comp);

            if (message === 0) {

                if (vm.viewModelHelper.modelIsValid) {

                    var changePasswordModel = { LoginID: '', OldPassword: vm.changePasswordModel.OldPassword, NewPassword: vm.changePasswordModel.NewPassword };


                    vm.viewModelHelper.apiPost('api/account/changepw', changePasswordModel,
                   function (result) {
                    window.location.href = '/Account/Login';
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else {
                    vm.viewModelHelper.modelErrors = vm.changePassword.errors;

                    var errorList = '';

                    angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                        errorList += error + '<br>';
                    });

                    toastr.error(errorList, 'Fintrak');
                }
            }

            else if (message === 2) {
                alert('Password Mismatch')
            }
            else {

                alert('New Password cannot be same as Old Password')
            }

        }

        var validate = function (OldPassword, NewPassword, Confirmpassword) {

            if (NewPassword === Confirmpassword && OldPassword !== NewPassword) {
                message = 0
            }

            else if (OldPassword === NewPassword) {
                message = 1
            }
            else {
                message = 2
            }
        }

        vm.checkpw = function scorePassword(pass) {
            var score = 0;
            if (!pass)
                return

            // award every unique letter until 5 repetitions
            var letters = new Object();
            for (var i = 0; i < pass.length; i++) {
                letters[pass[i]] = (letters[pass[i]] || 0) + 1;
                score += 5.0 / letters[pass[i]];
            }

            // bonus points for mixing it up
            var variations = {
                digits: /\d/.test(pass),
                lower: /[a-z]/.test(pass),
                upper: /[A-Z]/.test(pass),
                nonWords: /\W/.test(pass),
            }

            var variationCount = 0;
            for (var check in variations) {
                variationCount += (variations[check] == true) ? 1 : 0;
            }
            score += (variationCount - 1) * 10;

            if (score > 80)
                turnGreen();
            if (score >= 30 && score <= 60)
                turnBlue();
            if (score >= 10 && score <= 30)
                turnYellow();
            if (score >= 0 && score <= 10)
                turnRed();
        }

        var turnGreen = function () {
            $scope.customStyle.style = { "color": "green" };
            vm.strength = 'Very Strong Password'
            vm.status = false;
            $scope.disabled = false;
        }

        var turnBlue = function () {
            $scope.customStyle.style = { "color": "blue" }
            vm.strength = 'Average Password'
            vm.status = false;
            $scope.disabled = false;
        }

        var turnYellow = function () {
            $scope.customStyle.style = { "color": "purple" }
            vm.strength = 'Weak Password'
            vm.status = true;
            $scope.disabled = true;
        }

        var turnRed = function () {
            $scope.customStyle.style = { "color": "red" }
            vm.strength = 'Very Weak Password'
            vm.status = true;
            $scope.disabled = true;
        }

        setupRules();
        initialize();
    }
}());

