/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProductTypeMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProductTypeMappingEditController]);

    function ProductTypeMappingEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'producttypemapping-edit-view';
        vm.viewName = 'Product Type Mapping';

        vm.productTypeMapping = {};

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var productTypeMappingRules = [];

        var setupRules = function () {
            productTypeMappingRules.push(new validator.PropertyRule("ProductType", {
                required: { message: "Product Type is required" }
            }));

            productTypeMappingRules.push(new validator.PropertyRule("ProductCode", {
                notZero: { message: "Product is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                getProductTypes();

                if ($stateParams.producttypemappingId !== 0) {
                    vm.viewModelHelper.apiGet('api/producttypemapping/getproducttypemapping/' + $stateParams.producttypemappingId, null,
                   function (result) {
                       vm.productTypeMapping = result.data;
                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.productTypeMapping = {  ProductCode: $stateParams.productcode, ProductType: '', Active: true };
            }
        }

        var initialView = function () {
         
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.productTypeMapping, productTypeMappingRules);
            vm.viewModelHelper.modelIsValid = vm.productTypeMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.productTypeMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/producttypemapping/updateproducttypemapping', vm.productTypeMapping,
               function (result) {
                   
                   $state.go('core-product-edit', { productId: $stateParams.productId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.productTypeMapping.errors;

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
                vm.viewModelHelper.apiPost('api/producttypemapping/deleteproducttypemapping', vm.productTypeMapping.ProductTypeMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-product-edit', { productId: $stateParams.productId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('core-product-edit', { productId: $stateParams.productId });
        };

        var getProductTypes = function () {
            vm.viewModelHelper.apiGet('api/producttype/availableproducttypes', null,
                 function (result) {
                     vm.productTypes = result.data;
                     initialView();
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load product types.', 'Fintrak');
                 }, null);
        }
        
        setupRules();
        initialize(); 
    }
}());
