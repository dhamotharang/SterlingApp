/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProcessRoleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProcessRoleEditController]);

    function ProcessRoleEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'processrole-edit-view';
        vm.viewName = 'Process Role';

        vm.processRole = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var processRoleRules = [];

        var setupRules = function () {
            processRoleRules.push(new validator.PropertyRule("RoleId", {
                required: { message: "Role is required" }
            }));

            processRoleRules.push(new validator.PropertyRule("ProcessId", {
                notZero: { message: "Process is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initializeLookUp();

                if ($stateParams.processroleId !== 0) {
                    vm.viewModelHelper.apiGet('api/processrole/getprocessrole/' + $stateParams.processroleId, null,
                   function (result) {
                       vm.processRole = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.processRole = {  ProcessId: $stateParams.processId, Active: true };
            }
        }

        var initializeLookUp = function () {
            getRoles();
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            //alert(vm.processRole.RoleId);
            validator.ValidateModel(vm.processRole, processRoleRules);
            vm.viewModelHelper.modelIsValid = vm.processRole.isValid;
            vm.viewModelHelper.modelErrors = vm.processRole.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/processrole/updateprocessrole', vm.processRole,
               function (result) {
                   
                   $state.go('core-process-edit', { processId: $stateParams.processId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.processRole.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm('Do');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/processrole/deleteprocessrole', vm.processRole.ProcessRoleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-process-edit', { processId: $stateParams.processId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-process-edit', { processId: $stateParams.processId });
        };

        vm.onChange = function () {
            //alert(vm.processRole.RoleId);
        }

        var getRoles = function () {
            vm.viewModelHelper.apiGet('api/role/availableroles', null,
                 function (result) {
                     vm.roles = result.data;
                     initialView();
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load roles.', 'Fintrak');
                 }, null);
        }
        
        setupRules();
        initialize(); 
    }
}());
