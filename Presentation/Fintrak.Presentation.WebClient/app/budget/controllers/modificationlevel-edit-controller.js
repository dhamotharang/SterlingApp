/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ModificationLevelEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ModificationLevelEditController]);

    function ModificationLevelEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'modificationlevel-edit-view';
        vm.viewName = 'Modification Level';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.modificationLevel = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       
        var modificationLevelRules = [];

        var setupRules = function () {

            modificationLevelRules.push(new validator.PropertyRule("ModuleCode", {
                required: { message: "Module is required" }
            }));

            modificationLevelRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Definition is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initializeLookUp();

                if ($stateParams.modificationLevelId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/modificationLevel/getmodificationLevel/' + $stateParams.modificationLevelId, null,
                   function (result) {
                       vm.modificationLevel = result.data;
                    
                       getOperationReviews(vm.modificationLevel.Year);

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.modificationLevel = { ModuleCode:'', DefinitionCode: '', Active: true };
            }
        }

        var initialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {
                
                
            }, 50);
        }

        var initializeLookUp = function(){
            getModules();
            getDefinitions();
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.modificationLevel, modificationLevelRules);
            vm.viewModelHelper.modelIsValid = vm.modificationLevel.isValid;
            vm.viewModelHelper.modelErrors = vm.modificationLevel.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/modificationLevel/updatemodificationLevel', vm.modificationLevel,
               function (result) {
                   
                   $state.go('budget-modificationlevel-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.modificationLevel.errors;

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
                vm.viewModelHelper.apiPost('api/modificationLevel/deletemodificationLevel', vm.modificationLevel.ModificationLevelId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-modificationlevel-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('budget-modificationlevel-list');
        };

        vm.operationChanged = function (operation) {
            getOperationReviews(operation);
        }

        var getModules = function () {
            vm.viewModelHelper.apiGet('api/budgetmodule/availablemodules', null,
                 function (result) {
                     vm.modules = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getDefinitions = function () {
            vm.viewModelHelper.apiGet('api/budget/teamdefinition/availableteamDefinitions', null,
                 function (result) {
                     vm.definitions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
