/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeCentralVaultScheduleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeCentralVaultScheduleEditController]);

    function IncomeCentralVaultScheduleEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'incomeCentralVaultSchedule-edit-view';
        vm.viewName = 'Income Central Vault Schedule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.currencies = [];
        vm.branches = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomecentralvaultscheduleRules = [];

        var setupRules = function () {

            incomecentralvaultscheduleRules.push(new validator.PropertyRule("BranchCode", {
                required: { message: "Branch Code is required" }
            }));

            incomecentralvaultscheduleRules.push(new validator.PropertyRule("Currency", {
                required: { message: "Currency is required" }
            }));

            incomecentralvaultscheduleRules.push(new validator.PropertyRule("Volume", {
                required: { message: "Volume is required" }
            }));

            incomecentralvaultscheduleRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            incomecentralvaultscheduleRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.incomeCentralVaultScheduleId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomecentralvaultschedule/getincomecentralvaultschedule/' + $stateParams.incomeCentralVaultScheduleId, null,
                   function (result) {
                       vm.incomecentralvaultschedule = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomecentralvaultschedule = { BranchCode: '', Currency: '', Volume: '', Year: '',Period: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getBranches();
            getCurrencies();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomecentralvaultschedule, incomecentralvaultscheduleRules);
            vm.viewModelHelper.modelIsValid = vm.incomecentralvaultschedule.isValid;
            vm.viewModelHelper.modelErrors = vm.incomecentralvaultschedule.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomecentralvaultschedule/updateincomecentralvaultschedule', vm.incomecentralvaultschedule,
               function (result) {
                   
                   $state.go('mpr-incomecentralvaultschedule-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomecentralvaultschedule.errors;

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
                vm.viewModelHelper.apiPost('api/incomecentralvaultschedule/deleteincomecentralvaultschedule', vm.incomecentralvaultschedule.incomecentralvaultscheduleId,//vm.incomecentralvaultschedule.incomecentralvaultscheduleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomecentralvaultschedule-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomecentralvaultschedule-list');
        };

        var getBranches = function () {
            vm.viewModelHelper.apiGet('api/branch/availablebranches', null,
                 function (result) {
                     vm.branches = result.data;
                 },
                 function (result) {
                     toastr.error('Unable to load branches', 'Fintrak');
                 }, null);
        }

        var getCurrencies = function () {
            vm.viewModelHelper.apiGet('api/currency/getbasecurrency', null,
                 function (result) {
                     vm.currencies = result.data;
                     vm.init === true;
                 },
                 function (result) {
                     toastr.error('Unable to load currencies', 'Fintrak');
                 }, null);
        }
        setupRules();
        initialize();
    }
}());
