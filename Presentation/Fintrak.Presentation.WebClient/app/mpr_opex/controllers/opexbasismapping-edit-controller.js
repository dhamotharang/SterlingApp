/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexBasisMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexBasisMappingEditController]);

    function OpexBasisMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexbasismapping-edit-view';
        vm.viewName = 'Opex Basis Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexBasisMapping = {};
        vm.basises = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexbasismappingRules = [];

        var setupRules = function () {

            //opexbasismappingRules.push(new validator.PropertyRule("Code", {
            //    required: { message: "Code is required" }
            //}));

            //opexbasismappingRules.push(new validator.PropertyRule("Name", {
            //    required: { message: "Name is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.opexbasismappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexbasismapping/getopexbasismapping/' + $stateParams.opexbasismappingId, null,
                   function (result) {
                       vm.opexBasisMapping = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.opexBasisMapping = { Active: true };
            }
        }

        var intializeLookUp = function () {
            getBasises();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexBasisMapping, vm.opexBasisMapping);
            vm.viewModelHelper.modelIsValid = vm.opexBasisMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.opexBasisMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexbasismapping/updateopexbasismapping', vm.opexBasisMapping,
               function (result) {
                   
                   $state.go('mpr-opexbasismapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexBasisMapping.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/opexbasismapping/deleteopexbasismapping', vm.opexBasisMapping.OpexBasisMappingId,//vm.opexBasisMapping.opexbasismappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexbasismapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-opexbasismapping-list');
        };

        var getBasises = function () {
            vm.viewModelHelper.apiGet('api/expensebasis/availableexpenseBasis', null,
                 function (result) {
                     vm.basises = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load expense basis.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
