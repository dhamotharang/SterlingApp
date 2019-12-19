/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExpenseProductMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExpenseProductMappingEditController]);

    function ExpenseProductMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'expenseproductmapping-edit-view';
        vm.viewName = 'Expense ProductMapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.expenseProductMappings = {};
        vm.Names = [];
        vm.Products = [];
       
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var expenseproductmappingRules = [];

        var setupRules = function () {

            expenseproductmappingRules.push(new validator.PropertyRule("BasisCode", {
                required: { message: "BasisCode is required" }
            }));

            expenseproductmappingRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "ProductCode is required" }
            }));
    
            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.expenseproductId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/expenseproductmapping/getexpenseproductmapping/' + $stateParams.expenseproductId, null,
                   function (result) {
                       vm.expenseProductMapping = result.data;
                       getProduct();
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.expenseProductMapping = { BasisCode: '', ProductCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getName();
            getProduct();

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.expenseProductMapping, expenseproductmappingRules);
            vm.viewModelHelper.modelIsValid = vm.expenseProductMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.expenseProductMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/expenseproductmapping/updateexpenseproductmapping', vm.expenseProductMapping,
               function (result) {
                   
                   $state.go('mpr-expenseproductmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.expenseProductMapping.errors;

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
                vm.viewModelHelper.apiPost('api/expenseproductmapping/deleteexpenseproductmapping', vm.expenseProductMapping.ExpenseProductId,//vm.expenseProductMapping.expenseproductmappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-expenseproductmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-expenseproductmapping-list');
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


        var getProduct = function () {
            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                 function (result) {
                     vm.Products = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
