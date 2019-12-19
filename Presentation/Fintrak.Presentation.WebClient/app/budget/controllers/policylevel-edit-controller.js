/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PolicyLevelEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PolicyLevelEditController]);

    function PolicyLevelEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'policylevel-edit-view';
        vm.viewName = 'Policy Level';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.policyLevel = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       
        var policyLevelRules = [];

        vm.centres = [
           { Code: 1, Name: 'Cost Centre' },
            { Code: 2, Name: 'Profit Centre' }
        ];

        var setupRules = function () {

            policyLevelRules.push(new validator.PropertyRule("ModuleCode", {
                required: { message: "Module is required" }
            }));

            policyLevelRules.push(new validator.PropertyRule("PolicyCode", {
                required: { message: "Policy is required" }
            }));

            policyLevelRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Definition is required" }
            }));

            policyLevelRules.push(new validator.PropertyRule("Center", {
                required: { message: "Center is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initializeLookUp();

                if ($stateParams.policyLevelId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/policyLevel/getpolicyLevel/' + $stateParams.policyLevelId, null,
                   function (result) {
                       vm.policyLevel = result.data;
                    
                       getOperationReviews(vm.policyLevel.Year);

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.policyLevel = { ModuleCode:'',PolicyCode:'', DefinitionCode: '', Centre: 1,ReviewCode:'',Year:'', Active: true };
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
            getPolicies();
            getDefinitions();
            getOperations();
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.policyLevel, policyLevelRules);
            vm.viewModelHelper.modelIsValid = vm.policyLevel.isValid;
            vm.viewModelHelper.modelErrors = vm.policyLevel.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/policyLevel/updatepolicyLevel', vm.policyLevel,
               function (result) {
                   
                   $state.go('budget-policylevel-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.policyLevel.errors;

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
                vm.viewModelHelper.apiPost('api/policyLevel/deletepolicyLevel', vm.policyLevel.PolicyLevelId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-policylevel-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('budget-policylevel-list');
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

        var getPolicies = function () {
            vm.viewModelHelper.apiGet('api/policy/availablepolicies', null,
                 function (result) {
                     vm.policies = result.data;
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

        var getOperations = function () {
            vm.viewModelHelper.apiGet('api/operation/availableoperations', null,
                 function (result) {
                     vm.operations = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getOperationReviews = function (operation) {
            vm.viewModelHelper.apiGet('api/operationreview/getoperationreviewbyoperation/' + operation, null,
                 function (result) {
                     vm.operationReviews = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
