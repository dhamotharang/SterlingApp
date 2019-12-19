/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCentralVaultAccountsEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCentralVaultAccountsEditController]);

    function IncomeCentralVaultAccountsEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'incomecentralvaultaccounts-edit-view';
        vm.viewName = 'Income Central Vault Accounts';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeCentralVaultAccounts = {};

        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomecentralvaultaccountsRules = [];

        var setupRules = function () {

            incomecentralvaultaccountsRules.push(new validator.PropertyRule("Account", {
                required: { message: "Account is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.incomecentralvaultaccountsId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecentralvaultaccounts/getincomecentralvaultaccounts/' + $stateParams.incomecentralvaultaccountsId, null,
                   function (result) {
                       vm.incomeCentralVaultAccounts = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeCentralVaultAccounts = { Account: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeCentralVaultAccounts, incomecentralvaultaccountsRules);
            vm.viewModelHelper.modelIsValid = vm.incomeCentralVaultAccounts.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeCentralVaultAccounts.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecentralvaultaccounts/updateincomecentralvaultaccounts', vm.incomeCentralVaultAccounts,
               function (result) {

                   $state.go('mpr-incomecentralvaultaccounts-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeCentralVaultAccounts.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/incomecentralvaultaccounts/deleteincomecentralvaultaccounts', vm.incomeCentralVaultAccounts.IncomeCentralVaultAccountsId,//vm.incomeCentralVaultAccounts.incomecentralvaultaccountsId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomecentralvaultaccounts-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecentralvaultaccounts-list');
        };

        setupRules();
        initialize();
    }
}());
