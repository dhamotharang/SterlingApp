/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroeconomicsVariableScenarioEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MacroeconomicsVariableScenarioEditController]);

    function MacroeconomicsVariableScenarioEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomicsvariablescenario-edit-view';
        vm.viewName = 'Macroeconomics Variable Scenario';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macroeconomicsVariableScenarios = {};
        vm.microeconomics = [];
        vm.microeconomic = [];

        vm.types = [
          { Id: 1, Name: 'MacroeconomicsVariable' },
          { Id: 2, Name: 'NPL-MacroeconomicsVariable' }
        ];

        vm.frequencies = [
            { Id: 'M', Name: 'Monthly' },
            { Id: 'Q', Name: 'Quaterly' },
            { Id: 'Y', Name: 'Yearly' }
        ];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var macroeconomicsvariablescenarioRules = [];

        var setupRules = function () {

            //macroeconomicsvariablescenarioRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.MacroeconomicsId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macroeconomicsvariablescenario/getmacroeconomicsvariablescenario/' + $stateParams.MacroeconomicsId, null,
                   function (result) {
                       vm.macroeconomicsVariableScenarios = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.macroeconomicsVariableScenarios = { Active: true };
            }
        }

        var intializeLookUp = function () {
            getMicroeconomics()
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.macroeconomicsVariableScenarios, macroeconomicsvariablescenarioRules);
            vm.viewModelHelper.modelIsValid = vm.macroeconomicsVariableScenarios.isValid;
            vm.viewModelHelper.modelErrors = vm.macroeconomicsVariableScenarios.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/macroeconomicsvariablescenario/updatemacroeconomicsvariablescenario', vm.macroeconomicsVariableScenarios,
               function (result) {
                   
                   $state.go('ifrs9-macroeconomicsvariablescenario-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.macroeconomicsVariableScenarios.errors;

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
                vm.viewModelHelper.apiPost('api/macroeconomicsvariablescenario/deletemacroeconomicsvariablescenario', vm.macroeconomicsVariableScenarios.MacroeconomicsId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-macroeconomicsvariablescenario-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-macroeconomicsvariablescenario-list');
        };


        var getMicroeconomics = function () {
            vm.viewModelHelper.apiGet('api/macroeconomicvariable/availablemacroEconomicVariables', null,
                 function (result) {
                     vm.microeconomics = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        
        setupRules();
        initialize(); 
    }
}());
