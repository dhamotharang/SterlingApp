/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RevacctRegistryEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RevacctRegistryEditController]);

    function RevacctRegistryEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS Core';
        vm.view = 'revacctregistry-edit-view';
        vm.viewName = 'Revenue Registry';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.RevacctRegistry = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.parents = [];
        vm.finTypes = [];
        vm.finSubTypes = [];
       
        var RevacctRegistryRules = [];

        var setupRules = function () {
          
            RevacctRegistryRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));

            RevacctRegistryRules.push(new validator.PropertyRule("FinType", {
                required: { message: "FinType is required" }
            }));

            RevacctRegistryRules.push(new validator.PropertyRule("FinSubType", {
                required: { message: "FinSubType is required" }
            }));

            RevacctRegistryRules.push(new validator.PropertyRule("Position", {
                notZero: { message: "Position is required" }
            }));

            RevacctRegistryRules.push(new validator.PropertyRule("Class", {
                notZero: { message: "Class is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.revenueId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/revacctregistry/getrevacctregistry/' + $stateParams.revenueId, null,
                   function (result) {
                       vm.revacctregistry = result.data;

                       initialView();

                       getFinSubTypes(vm.revacctregistry.FinType);
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.revacctregistry = { Caption: '', FinType: '',FinSubType:'', Position: 1,Class: 1, Active: true };
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
            validator.ValidateModel(vm.revacctregistry, RevacctRegistryRules);
            vm.viewModelHelper.modelIsValid = vm.revacctregistry.isValid;
            vm.viewModelHelper.modelErrors = vm.revacctregistry.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/revacctregistry/updaterevacctregistry', vm.revacctregistry,
               function (result) {
                   
                   $state.go('finstat-revacctregistry-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.revacctregistry.errors;

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
                vm.viewModelHelper.apiPost('api/revacctregistry/deleterevacctregistry', vm.revacctregistry.RevenueId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-revacctregistry-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-revacctregistry-list');
        };

        vm.onFinTypeChanged = function (finType) {
            getFinSubTypes(finType);
        }

        var getParents = function () {
            vm.viewModelHelper.apiGet('api/revacctregistry/availablerevacctregistrys', null,
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
