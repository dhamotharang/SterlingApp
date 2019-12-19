/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GenderGroupEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GenderGroupEditController]);

    function GenderGroupEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CDQM';
        vm.view = 'gendergroup-edit-view';
        vm.viewName = 'Gender Group';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.genderGroup = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var genderGroupRules = [];

        var setupRules = function () {
          
            
            //genderGroupRules.push(new validator.PropertyRule("StreetName", {
            //    required: { message: "Street Name is required" }
            //}));

          

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.gendergroupId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/cdqmgenderGroup/getcdqmgenderGroup/' + $stateParams.gendergroupId, null,
                   function (result) {
                       vm.genderGroup = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.genderGroup = { Title: '', GroupGender: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.genderGroup, genderGroupRules);
            vm.viewModelHelper.modelIsValid = vm.genderGroup.isValid;
            vm.viewModelHelper.modelErrors = vm.genderGroup.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/cdqmgenderGroup/updatecdqmgenderGroup', vm.genderGroup,
               function (result) {
                   
                   $state.go('cdqm-gendergroup-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.genderGroup.errors;

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
                vm.viewModelHelper.apiPost('api/cdqmgenderGroup/deletecdqmgenderGroup', vm.genderGroup.GenderGroupId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('cdqm-gendergroup-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('cdqm-gendergroup-list');
        };

        setupRules();
        initialize(); 
    }
}());
