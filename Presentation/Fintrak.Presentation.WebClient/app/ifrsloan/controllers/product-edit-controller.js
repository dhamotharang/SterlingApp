/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IFRSProductEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IFRSProductEditController]);

    function IFRSProductEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'ifrsproduct-edit-view';
        vm.viewName = 'Product';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsProduct = {};

        vm.products = [];
        vm.scheduleTypes = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ifrsproductRules = [];

        var setupRules = function () {
          
            ifrsproductRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product is required" }
            }));

            ifrsproductRules.push(new validator.PropertyRule("ScheduleTypeCode", {
                required: { message: "Schedule Type is required" }
            }));
            ifrsproductRules.push(new validator.PropertyRule("MarketRate", {
                required: { message: "Market Rate is required" }
            }));
            //ifrsproductRules.push(new validator.PropertyRule("PastDueRate", {
            //    required: { message: "Past Due Rate is required" }
            //}));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.productId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrsproduct/getproduct/' + $stateParams.productId, null,
                   function (result) {
                       vm.ifrsProduct = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsProduct = { ProductCode: $stateParams.code, ScheduleTypeCode: '', MarketRate: '', PastDueRate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
        getProducts();
           // getLoanProducts();
        getScheduleTypes();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsProduct, ifrsproductRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsProduct.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsProduct.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrsproduct/updateproduct', vm.ifrsProduct,
               function (result) {
                   
                   $state.go('ifrsloan-product-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsProduct.errors;

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
                vm.viewModelHelper.apiPost('api/ifrsproduct/deleteproduct', vm.ifrsProduct.ProductId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-product-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrsloan-product-list');
        };

        var getProducts = function () {
            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                 function (result) {
                     vm.products = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        //var getLoanProducts = function () {
        //    vm.viewModelHelper.apiGet('api/ifrsproduct/getloanproduct',  null,
        //         function (result) {
        //             vm.products = result.data;
        //         },
        //         function (result) {

        //         }, null);
        //}

        var getScheduleTypes = function () {
            vm.viewModelHelper.apiGet('api/scheduletype/availablescheduletypes', null,
                 function (result) {
                     vm.scheduleTypes = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load schedule types.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
