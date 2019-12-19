/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCashCentreCodeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCashCentreCodeEditController]);

    function IncomeCashCentreCodeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'incomecashcentrecode-edit-view';
        vm.viewName = 'Income CashCentre Code';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeCashCentreCode = {};

        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomecashcentrecodeRules = [];

        var setupRules = function () {

            incomecashcentrecodeRules.push(new validator.PropertyRule("CashCentreCode", {
                required: { message: "CashCentre Code is required" }
            }));

            incomecashcentrecodeRules.push(new validator.PropertyRule("BranchCode", {
                required: { message: "Branch Code is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.incomecashcentrecodeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecashcentrecode/getincomecashcentrecode/' + $stateParams.incomecashcentrecodeId, null,
                   function (result) {
                       vm.incomeCashCentreCode = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeCashCentreCode = { CashCentreCode: '', BranchCode: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeCashCentreCode, incomecashcentrecodeRules);
            vm.viewModelHelper.modelIsValid = vm.incomeCashCentreCode.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeCashCentreCode.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecashcentrecode/updateincomecashcentrecode', vm.incomeCashCentreCode,
               function (result) {

                   $state.go('mpr-incomecashcentrecode-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeCashCentreCode.errors;

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
                vm.viewModelHelper.apiPost('api/incomecashcentrecode/deleteincomecashcentrecode', vm.incomeCashCentreCode.IncomeCashCentreCodeId,//vm.incomeCashCentreCode.incomecashcentrecodeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomecashcentrecode-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecashcentrecode-list');
        };

        setupRules();
        initialize();
    }
}());
