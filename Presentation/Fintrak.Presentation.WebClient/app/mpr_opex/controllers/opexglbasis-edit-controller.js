/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexGLBasisEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexGLBasisEditController]);

    function OpexGLBasisEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexglbasis-edit-view';
        vm.viewName = 'Opex GL Basis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexGLBasis = {};
        vm.basises = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexglbasisRules = [];

        var setupRules = function () {

            //opexglbasisRules.push(new validator.PropertyRule("Code", {
            //    required: { message: "Code is required" }
            //}));

            //opexglbasisRules.push(new validator.PropertyRule("Name", {
            //    required: { message: "Name is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.opexglbasisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexglbasis/getopexglbasis/' + $stateParams.opexglbasisId, null,
                   function (result) {
                       vm.opexGLBasis = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.opexGLBasis = { Active: true };
            }
        }

        var intializeLookUp = function () {
            getBasises();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexGLBasis, vm.opexGLBasis);
            vm.viewModelHelper.modelIsValid = vm.opexGLBasis.isValid;
            vm.viewModelHelper.modelErrors = vm.opexGLBasis.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexglbasis/updateopexglbasis', vm.opexGLBasis,
               function (result) {
                   
                   $state.go('mpr-opexglbasis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexGLBasis.errors;

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
                vm.viewModelHelper.apiPost('api/opexglbasis/deleteopexglbasis', vm.opexGLBasis.OpexGLBasisId,//vm.opexGLBasis.opexglbasisId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexglbasis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-opexglbasis-list');
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
