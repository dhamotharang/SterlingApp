/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("macrovariablerecoveryratesEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        macrovariablerecoveryratesEditController]);

    function macrovariablerecoveryratesEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macrovariablerecoveryrates-edit-view';
        vm.viewName = 'Lgd Scenarios for an Instrument';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macroVarRecoveryRates = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var macroVarRecoveryRatesRules = [];
        
        var setupRules = function () {

            macroVarRecoveryRatesRules.push(new validator.PropertyRule("InstrumentType", {
                required: { message: "Instrument type is required" }
            }));

            macroVarRecoveryRatesRules.push(new validator.PropertyRule("LGD_BEST", {
                required: { message: "LGD (BEST) is required" }
            }));

            macroVarRecoveryRatesRules.push(new validator.PropertyRule("LGD_DOWNTURN", {
                required: { message: "LGD (Downturn) is required" }
            }));

            macroVarRecoveryRatesRules.push(new validator.PropertyRule("LGD_OPTIMISTIC", {
                required: { message: "LGD (Optimistic) is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.instrumentId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macrovariablerecoveryrates/getMacroVarRecoveryRatesById/' + $stateParams.instrumentId, null,
                   function (result) {
                       vm.macroVarRecoveryRates = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.macroVarRecoveryRates = { Seq: 0, BestEstimate: 0, Optimistic: 0, Downturn: 0, Date: new Date(), Active: true };
            }
        }

        var intializeLookUp = function () {

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.macroVarRecoveryRates, macroVarRecoveryRatesRules);
            vm.viewModelHelper.modelIsValid = vm.macroVarRecoveryRates.isValid;
            vm.viewModelHelper.modelErrors = vm.macroVarRecoveryRates.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/macrovariablerecoveryrates/updateMacroVarRecoveryRates', vm.macroVarRecoveryRates,
               function (result) {

                   $state.go('ifrs9-macrovariablerecoveryrates-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.macroVarRecoveryRates.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/macrovariablerecoveryrates/deleteMacroVarRecoveryRates', vm.macroVarRecoveryRates.RecoveryRatesId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-macrovariablerecoveryrates-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-macrovariablerecoveryrates-list');
        };

        setupRules();
        initialize();
    }
}());
