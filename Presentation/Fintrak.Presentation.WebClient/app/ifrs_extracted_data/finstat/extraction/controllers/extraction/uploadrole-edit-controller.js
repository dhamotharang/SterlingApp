/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("UploadRoleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        UploadRoleEditController]);

    function UploadRoleEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'uploadrole-edit-view';
        vm.viewName = 'Upload Role';

        vm.uploadRole = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var uploadRoleRules = [];

        var setupRules = function () {
            uploadRoleRules.push(new validator.PropertyRule("RoleId", {
                required: { message: "Role is required" }
            }));

            uploadRoleRules.push(new validator.PropertyRule("UploadId", {
                notZero: { message: "Upload is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initializeLookUp();

                if ($stateParams.uploadroleId !== 0) {
                    vm.viewModelHelper.apiGet('api/uploadrole/getuploadrole/' + $stateParams.uploadroleId, null,
                   function (result) {
                       vm.uploadRole = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.uploadRole = {  UploadId: $stateParams.uploadId,RoleId:0, Active: true };
            }
        }

        var initializeLookUp = function () {
            getRoles();
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            //alert(vm.uploadRole.RoleId);
            validator.ValidateModel(vm.uploadRole, uploadRoleRules);
            vm.viewModelHelper.modelIsValid = vm.uploadRole.isValid;
            vm.viewModelHelper.modelErrors = vm.uploadRole.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/uploadrole/updateuploadrole', vm.uploadRole,
               function (result) {
                   
                   $state.go('core-upload-edit', { uploadId: $stateParams.uploadId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.uploadRole.errors;

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
                vm.viewModelHelper.apiPost('api/uploadrole/deleteuploadrole', vm.uploadRole.UploadRoleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-upload-edit', { uploadId: $stateParams.uploadId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-upload-edit', { uploadId: $stateParams.uploadId });
        };

        vm.onChange = function () {
            //alert(vm.uploadRole.RoleId);
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
