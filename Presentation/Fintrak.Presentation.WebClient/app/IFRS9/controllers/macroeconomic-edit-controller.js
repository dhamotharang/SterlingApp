/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacroEconomicEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MacroEconomicEditController]);

    function MacroEconomicEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macroeconomic-edit-view';
        vm.viewName = 'Macro Economic';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macroEconomic = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var macroeconomicRules = [];
        vm.sectors = [];

        var setupRules = function () {
          
            macroeconomicRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            macroeconomicRules.push(new validator.PropertyRule("Sector_Code", {
                required: { message: "Sector_Code is required" }
            }));

            macroeconomicRules.push(new validator.PropertyRule("Variable1", {
                required: { message: "lst Variable is required" }
            }));

            macroeconomicRules.push(new validator.PropertyRule("Variable2", {
                required: { message: "2nd Variable is required" }
            }));



        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.macroeconomicId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macroeconomic/getmacroeconomic/' + $stateParams.macroeconomicId, null,
                   function (result) {
                       vm.macroEconomic = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.macroEconomic = { Year: 0, Sector_Code: '',Variable1: 0, Variable2: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getSectors()
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.macroEconomic, macroeconomicRules);
            vm.viewModelHelper.modelIsValid = vm.macroEconomic.isValid;
            vm.viewModelHelper.modelErrors = vm.macroEconomic.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/macroeconomic/updatemacroeconomic', vm.macroEconomic,
               function (result) {
                   
                   $state.go('ifrs9-macroeconomic-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.macroEconomic.errors;

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
                vm.viewModelHelper.apiPost('api/macroeconomic/deletemacroeconomic', vm.macroEconomic.MacroEconomicId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-macroeconomic-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-macroeconomic-list');
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

        setupRules();
        initialize(); 
    }
}());
