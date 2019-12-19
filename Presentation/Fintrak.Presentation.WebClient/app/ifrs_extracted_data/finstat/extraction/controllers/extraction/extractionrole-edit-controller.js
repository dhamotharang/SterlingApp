/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExtractionRoleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExtractionRoleEditController]);

    function ExtractionRoleEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'extractionrole-edit-view';
        vm.viewName = 'Extraction Role';

        vm.extractionRole = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var extractionRoleRules = [];

        var setupRules = function () {
            extractionRoleRules.push(new validator.PropertyRule("RoleId", {
                required: { message: "Role is required" }
            }));

            extractionRoleRules.push(new validator.PropertyRule("ExtractionId", {
                notZero: { message: "Extraction is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initializeLookUp();

                if ($stateParams.extractionroleId !== 0) {
                    vm.viewModelHelper.apiGet('api/extractionrole/getextractionrole/' + $stateParams.extractionroleId, null,
                   function (result) {
                       vm.extractionRole = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.extractionRole = {  ExtractionId: $stateParams.extractionId, Active: true };
            }
        }

        var initializeLookUp = function () {
            getRoles();
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            //alert(vm.extractionRole.RoleId);
            validator.ValidateModel(vm.extractionRole, extractionRoleRules);
            vm.viewModelHelper.modelIsValid = vm.extractionRole.isValid;
            vm.viewModelHelper.modelErrors = vm.extractionRole.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/extractionrole/updateextractionrole', vm.extractionRole,
               function (result) {
                   
                   $state.go('core-extraction-edit', { extractionId: $stateParams.extractionId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.extractionRole.errors;

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
                vm.viewModelHelper.apiPost('api/extractionrole/deleteextractionrole', vm.extractionRole.ExtractionRoleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-extraction-edit', { extractionId: $stateParams.extractionId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-extraction-edit', { extractionId: $stateParams.extractionId });
        };

        vm.onChange = function () {
            //alert(vm.extractionRole.RoleId);
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
