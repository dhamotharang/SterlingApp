/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SectorVariableMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SectorVariableMappingEditController]);

    function SectorVariableMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'sectorvariablemapping-edit-view';
        vm.viewName = 'Historical MacroEconomic Varaibales';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sectorVariableMapping = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var sectorvariablemappingRules = [];
        vm.sectors = [];
        vm.variables = [];

        vm.types = [
            { Id: 1, Name: 'PD' },
            { Id: 2, Name: 'LGD' },
        ];

        var setupRules = function () {
          
            sectorvariablemappingRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            sectorvariablemappingRules.push(new validator.PropertyRule("Sector", {
                required: { message: "Sector is required" }
            }));

            sectorvariablemappingRules.push(new validator.PropertyRule("Type", {
                required: { message: "Type is required" }
            }));

            sectorvariablemappingRules.push(new validator.PropertyRule("Variable", {
                required: { message: "Variable is required" }
            }));

            sectorvariablemappingRules.push(new validator.PropertyRule("Value", {
                required: { message: "Value is required" }
            }));



        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.sectorvariablemappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/sectorvariablemapping/getsectorvariablemapping/' + $stateParams.sectorvariablemappingId, null,
                   function (result) {
                       vm.sectorVariableMapping = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.sectorVariableMapping = { Year: 0, Sector: '', Type: 0, Variable: '', Value: 0, Active: true };
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
            validator.ValidateModel(vm.sectorVariableMapping, sectorvariablemappingRules);
            vm.viewModelHelper.modelIsValid = vm.sectorVariableMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.sectorVariableMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/sectorvariablemapping/updatesectorvariablemapping', vm.sectorVariableMapping,
               function (result) {
                   
                   $state.go('ifrs9-sectorvariablemapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.sectorVariableMapping.errors;

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
                vm.viewModelHelper.apiPost('api/sectorvariablemapping/deletesectorvariablemapping', vm.sectorVariableMapping.SectorVariableMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-sectorvariablemapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-sectorvariablemapping-list');
        };


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
