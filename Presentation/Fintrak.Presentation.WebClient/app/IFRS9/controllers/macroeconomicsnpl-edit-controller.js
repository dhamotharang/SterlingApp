/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroEconomicsNPLEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MacroEconomicsNPLEditController]);

    function MacroEconomicsNPLEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomicsnpl-edit-view';
        vm.viewName = 'MacroEconomicsNPL';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macroeconomicsnpl = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var macroeconomicsnplRules = [];


        var setupRules = function () {

            macroeconomicsnplRules.push(new validator.PropertyRule("Sequence", {
                required: { message: "Sequence is required" }
            }));

            macroeconomicsnplRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            macroeconomicsnplRules.push(new validator.PropertyRule("NPL", {
                required: { message: "NPL Ratio is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.macroeconomicnplId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macroeconomicsnpl/getmacroeconomicsnpl/' + $stateParams.macroeconomicnplId, null,
                   function (result) {
                       vm.macroeconomicsnpl = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.macroeconomicsnpl = { Seq: 0, Year: 0, NPL: 0, Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.macroeconomicsnpl, macroeconomicsnplRules);
            vm.viewModelHelper.modelIsValid = vm.macroeconomicsnpl.isValid;
            vm.viewModelHelper.modelErrors = vm.macroeconomicsnpl.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/macroeconomicsnpl/updatemacroeconomicsnpl', vm.macroeconomicsnpl,
               function (result) {

                   $state.go('ifrs9-macroeconomicsnpl-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.macroeconomicsnpl.errors;

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
                vm.viewModelHelper.apiPost('api/macroeconomicsnpl/deletemacroeconomicsnpl', vm.macroeconomicsnpl.Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-macroeconomicsnpl-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-macroeconomicsnpl-list');
        };

        setupRules();
        initialize();
    }
}());
