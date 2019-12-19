/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroEconomicVariableEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MacroEconomicVariableEditController]);

    function MacroEconomicVariableEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomicvariable-edit-view';
        vm.viewName = 'Macro Economic Varaibales';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macroEconomicVariable = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var macroeconomicvariableRules = [];

        var setupRules = function () {
          
            macroeconomicvariableRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            macroeconomicvariableRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

            macroeconomicvariableRules.push(new validator.PropertyRule("IsGeneric", {
                required: { message: "IsGeneric is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.macroeconomicvariableId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macroeconomicvariable/getmacroeconomicvariable/' + $stateParams.macroeconomicvariableId, null,
                   function (result) {
                       vm.macroEconomicVariable = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.macroEconomicVariable = { Name: '', Description: '', IsGeneric: false, Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.macroEconomicVariable, macroeconomicvariableRules);
            vm.viewModelHelper.modelIsValid = vm.macroEconomicVariable.isValid;
            vm.viewModelHelper.modelErrors = vm.macroEconomicVariable.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/macroeconomicvariable/updatemacroeconomicvariable', vm.macroEconomicVariable,
               function (result) {
                   
                   $state.go('ifrs9-macroeconomicvariable-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.macroEconomicVariable.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/macroeconomicvariable/deletemacroeconomicvariable', vm.macroEconomicVariable.MacroEconomicVariableId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-macroeconomicvariable-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-macroeconomicvariable-list');
        };


        setupRules();
        initialize(); 
    }
}());
