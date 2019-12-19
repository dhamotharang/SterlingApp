/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExpenseGLMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExpenseGLMappingEditController]);

    function ExpenseGLMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'expenseglmapping-edit-view';
        vm.viewName = 'Expense GLMapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.expenseGLMappings = {};
        //vm.parents = [];
        vm.Names = [];

       
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var expenseglmappingRules = [];

        var setupRules = function () {

            expenseglmappingRules.push(new validator.PropertyRule("BasisCode", {
                required: { message: "BasisCode is required" }
            }));

            expenseglmappingRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GLCode is required" }
            }));
            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.expenseglId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/expenseglmapping/getexpenseglmapping/' + $stateParams.expenseglId, null,
                   function (result) {
                       vm.expenseGLMapping = result.data;
                       getName(),
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.expenseGLMapping = { BasisCode: '', GLCode: '', Active: true };
            }
        }


        var intializeLookUp = function () {          
            getName();

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.expenseGLMapping, expenseglmappingRules);
            vm.viewModelHelper.modelIsValid = vm.expenseGLMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.expenseGLMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/expenseglmapping/updateexpenseglmapping', vm.expenseGLMapping,
               function (result) {
                   
                   $state.go('mpr-expenseglmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.expenseGLMapping.errors;

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
                vm.viewModelHelper.apiPost('api/expenseglmapping/deleteexpenseglmapping', vm.expenseGLMapping.ExpenseGLId,//vm.expenseGLMapping.expenseglmappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-expenseglmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-expenseglmapping-list');
        };


        var getName = function () {
            vm.viewModelHelper.apiGet('api/expensebasis/availableexpenseBasis', null,
                 function (result) {
                     vm.Names = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load expense basis.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
