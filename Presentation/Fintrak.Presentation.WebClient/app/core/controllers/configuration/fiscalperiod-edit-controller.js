/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FiscalPeriodEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FiscalPeriodEditController]);

    function FiscalPeriodEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'fiscalperiod-edit-view';
        vm.viewName = 'Fiscal Period';

        vm.fiscalPeriod = {};

        vm.openedStartDate = false;
        vm.openedEndDate = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var fiscalPeriodRules = [];

        var setupRules = function () {
            fiscalPeriodRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            fiscalPeriodRules.push(new validator.PropertyRule("StartDate", {
                mustBeDate: { message: "Please enter a valid date" }
            }));

            fiscalPeriodRules.push(new validator.PropertyRule("EndDate", {
                mustBeDate: { message: "Please enter a valid date" }
            }));

            fiscalPeriodRules.push(new validator.PropertyRule("FiscalYearId", {
                notZero: { message: "Fiscal Year is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                if ($stateParams.fiscalperiodId !== 0) {
                    vm.viewModelHelper.apiGet('api/fiscalperiod/getfiscalperiod/' + $stateParams.fiscalperiodId, null,
                   function (result) {
                       vm.fiscalPeriod = result.data;
                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.fiscalPeriod = { Name: '', StartDate: new Date(), EndDate: new Date(), FiscalYearId: $stateParams.fiscalyearId, Closed: false, Active: true };
            }
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.fiscalPeriod, fiscalPeriodRules);
            vm.viewModelHelper.modelIsValid = vm.fiscalPeriod.isValid;
            vm.viewModelHelper.modelErrors = vm.fiscalPeriod.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/fiscalperiod/updatefiscalperiod', vm.fiscalPeriod,
               function (result) {
                   
                   $state.go('core-fiscalyear-edit', { fiscalyearId: $stateParams.fiscalyearId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.fiscalPeriod.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm('Do');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/fiscalperiod/deletefiscalperiod', vm.fiscalPeriod.FiscalPeriodId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-fiscalyear-edit', { fiscalyearId: $stateParams.fiscalyearId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-fiscalyear-edit', { fiscalyearId: $stateParams.fiscalyearId });
        };

        vm.openStartDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedStartDate = true;
        }

        vm.openEndDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedEndDate = true;
        }

        setupRules();
        initialize(); 
    }
}());
