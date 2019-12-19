/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RegistryEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RegistryEditController]);

    function RegistryEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'registry-edit-view';
        vm.viewName = 'Registry';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.registry = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.parents = [];
        vm.finTypes = [];
        vm.finSubTypes = [];
       
        var registryRules = [];

        var setupRules = function () {
          
            registryRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));

            registryRules.push(new validator.PropertyRule("FinType", {
                required: { message: "FinType is required" }
            }));

            registryRules.push(new validator.PropertyRule("FinSubType", {
                required: { message: "FinSubType is required" }
            }));

            registryRules.push(new validator.PropertyRule("Position", {
                notZero: { message: "Position is required" }
            }));

            registryRules.push(new validator.PropertyRule("Class", {
                notZero: { message: "Class is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.registryId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/registry/getregistry/' + $stateParams.registryId, null,
                   function (result) {
                       vm.registry = result.data;

                       initialView();

                       getFinSubTypes(vm.registry.FinType);
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.registry = { Caption: '', FinType: '',FinSubType:'', Position: 1,Class: 1, Active: true };
            }
        }

        var intializeLookUp = function () {
            getParents();
            getFinTypes();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.registry, registryRules);
            vm.viewModelHelper.modelIsValid = vm.registry.isValid;
            vm.viewModelHelper.modelErrors = vm.registry.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/registry/updateregistry', vm.registry,
               function (result) {
                   
                   $state.go('finstat-registry-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.registry.errors;

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
                vm.viewModelHelper.apiPost('api/registry/deleteregistry', vm.registry.RegistryId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-registry-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-registry-list');
        };

        vm.onFinTypeChanged = function (finType) {
            getFinSubTypes(finType);
        }

        var getParents = function () {
            vm.viewModelHelper.apiGet('api/registry/availableregistrys', null,
                 function (result) {
                     vm.parents = result.data;
                 },
                 function (result) {
                     toastr.error('Unable to load parents', 'Fintrak');
                 }, null);
        }

        var getFinTypes = function () {
            vm.viewModelHelper.apiGet('api/financialtype/getFinTypes', null,
                 function (result) {
                     vm.finTypes = result.data;
                 },
                 function (result) {
                     toastr.error('Unable to load financial types', 'Fintrak');
                 }, null);
        }

        var getFinSubTypes = function (finType) {
            vm.viewModelHelper.apiGet('api/financialtype/getFinSubTypes/' + finType, null,
                 function (result) {
                     vm.finSubTypes = result.data;
                 },
                 function (result) {
                     toastr.error('Unable to load financial sub types', 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
