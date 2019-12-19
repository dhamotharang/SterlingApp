/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexBranchMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexBranchMappingEditController]);

    function OpexBranchMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexbranchmapping-edit-view';
        vm.viewName = 'Opex Branch Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexBranchMapping = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexbranchmappingRules = [];

        var setupRules = function () {

            opexbranchmappingRules.push(new validator.PropertyRule("BranchCode", {
                required: { message: "BranchCode is required" }
            }));

            opexbranchmappingRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "MisCode is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.opexbranchmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexbranchmapping/getopexbranchmapping/' + $stateParams.opexbranchmappingId, null,
                   function (result) {
                       vm.opexBranchMapping = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.opexBranchMapping = { BranchCode: '', MisCode: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexBranchMapping, opexbranchmappingRules);
            vm.viewModelHelper.modelIsValid = vm.opexBranchMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.opexBranchMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexbranchmapping/updateopexbranchmapping', vm.opexBranchMapping,
               function (result) {

                   $state.go('mpr-opexbranchmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexBranchMapping.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');
            }

        }

        
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/opexbranchmapping/deleteopexbranchmapping', vm.opexBranchMapping.OpexBranchMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexbranchmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-opexbranchmapping-list');
        };

        setupRules();
        initialize();
    }
}());
