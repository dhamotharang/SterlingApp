/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProductTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ProductTypeEditController]);

    function ProductTypeEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'producttype-edit-view';
        vm.viewName = 'Product Type';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.productType = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var productTypeRules = [];

        var setupRules = function () {
          
            productTypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                if ($stateParams.producttypeId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/productType/getproductType/' + $stateParams.producttypeId, null,
                   function (result) {
                       vm.productType = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.productType = { Name: '', Active: true };
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.productType, productTypeRules);
            vm.viewModelHelper.modelIsValid = vm.productType.isValid;
            vm.viewModelHelper.modelErrors = vm.productType.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/productType/updateproductType', vm.productType,
               function (result) {
                   
                   $state.go('core-producttype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.productType.errors;

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
                vm.viewModelHelper.apiPost('api/productType/deleteproductType', vm.productType.ProductTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-producttype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-producttype-list');
        };

        setupRules();
        initialize(); 
    }
}());
