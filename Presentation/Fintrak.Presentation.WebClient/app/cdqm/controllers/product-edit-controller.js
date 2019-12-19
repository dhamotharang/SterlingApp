/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CDQMProductEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CDQMProductEditController]);

    function CDQMProductEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CDQM';
        vm.view = 'product-edit-view';
        vm.viewName = 'Product';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.product = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var productRules = [];

        var setupRules = function () {
          
            
            //productRules.push(new validator.PropertyRule("StreetName", {
            //    required: { message: "Street Name is required" }
            //}));

          

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.productId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/cdqmproduct/getcdqmproduct/' + $stateParams.productId, null,
                   function (result) {
                       vm.product = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.product = { ProductCode: '', ProductName: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.product, productRules);
            vm.viewModelHelper.modelIsValid = vm.product.isValid;
            vm.viewModelHelper.modelErrors = vm.product.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/cdqmproduct/updatecdqmproduct', vm.product,
               function (result) {
                   
                   $state.go('cdqm-product-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.product.errors;

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
                vm.viewModelHelper.apiPost('api/cdqmproduct/deletecdqmproduct', vm.product.ProductId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('cdqm-product-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('cdqm-product-list');
        };

        setupRules();
        initialize(); 
    }
}());
