/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroEconomicHistoricalEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MacroEconomicHistoricalEditController]);

    function MacroEconomicHistoricalEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomichistorical-edit-view';
        vm.viewName = 'Forecasted Macro Economic Variable';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macroEconomicHistorical = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var macroeconomichistoricalRules = [];
        vm.sectors = [];
        vm.variables = [];

        vm.types = [
            { Id: 1, Name: 'PD' },
            { Id: 2, Name: 'LGD' },
        ];


        var setupRules = function () {
          
            macroeconomichistoricalRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            macroeconomichistoricalRules.push(new validator.PropertyRule("Sector_Code", {
                required: { message: "Sector is required" }
            }));

            macroeconomichistoricalRules.push(new validator.PropertyRule("Type", {
                required: { message: "Type is required" }
            }));

            macroeconomichistoricalRules.push(new validator.PropertyRule("Value", {
                required: { message: "Value is required" }
            }));

            macroeconomichistoricalRules.push(new validator.PropertyRule("Variable", {
                required: { message: "Variable is required" }
            }));


        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.macroeconomichistoricalId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macroeconomichistorical/getmacroeconomichistorical/' + $stateParams.macroeconomichistoricalId, null,
                   function (result) {
                       vm.macroEconomicHistorical = result.data;
                       vm.ontypeChanged;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.macroEconomicHistorical = { Year: 0, Sector_Code: '', Type: '', Value: 0, Variable: '',  Active: true };
            }
        }

        var intializeLookUp = function () {
            getSectors()
            getVariables()
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.macroEconomicHistorical, macroeconomichistoricalRules);
            vm.viewModelHelper.modelIsValid = vm.macroEconomicHistorical.isValid;
            vm.viewModelHelper.modelErrors = vm.macroEconomicHistorical.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/macroeconomichistorical/updatemacroeconomichistorical', vm.macroEconomicHistorical,
               function (result) {
                   
                   $state.go('ifrs9-macroeconomichistorical-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.macroEconomicHistorical.errors;

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
                vm.viewModelHelper.apiPost('api/macroeconomichistorical/deletemacroeconomichistorical', vm.macroEconomicHistorical.MacroEconomicHistoricalId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-macroeconomichistorical-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-macroeconomichistorical-list');
        };


        //vm.ontypeChanged = function (type) {
        //    vm.usetypeId = false;
        //    if (type === 'PD')
        //        vm.usetypeId = false;
        //    else
        //        vm.usetypeId = true;

        //}


        var getSectors = function () {
            vm.viewModelHelper.apiGet('api/sector/availablesectors', null,
                 function (result) {
                     vm.sectors = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getVariables = function () {
            vm.viewModelHelper.apiGet('api/macroeconomicvariable/availablemacroEconomicVariables', null,
                 function (result) {
                     vm.variables = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        setupRules();
        initialize(); 
    }
}());
