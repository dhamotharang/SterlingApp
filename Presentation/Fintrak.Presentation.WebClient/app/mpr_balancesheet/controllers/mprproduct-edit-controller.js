/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRProductEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MPRProductEditController]);

    function MPRProductEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mprproduct-edit-view';
        vm.viewName = 'Product';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprProduct = {};

        vm.moduleOwners = [
           { Id: 1, Name: 'Generic' },
           { Id: 2, Name: 'MPR' },
            { Id: 3, Name: 'Budget' }
        ];

        vm.products = [];
        vm.captions = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var mprproductRules = [];

        var setupRules = function () {
          
            mprproductRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product is required" }
            }));

            mprproductRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.mprproductId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mprproduct/getmprproduct/' + $stateParams.mprproductId, null,
                   function (result) {
                       vm.mprProduct = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.mprProduct = { ProductCode: '', CaptionCode: '', VolumeGL: '', InterestGL: '', Budgetable: true, IsNotational: false,Rate: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getProducts();
            getCaptions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.mprProduct, mprproductRules);
            vm.viewModelHelper.modelIsValid = vm.mprProduct.isValid;
            vm.viewModelHelper.modelErrors = vm.mprProduct.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/mprproduct/updatemprproduct', vm.mprProduct,
               function (result) {
                   
                   $state.go('mpr-mprproduct-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.mprProduct.errors;

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
                vm.viewModelHelper.apiPost('api/mprproduct/deletemprproduct', vm.mprProduct.MPRProductId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-mprproduct-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-mprproduct-list');
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

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/bscaption/availablebscaptions', null,
                 function (result) {
                     vm.captions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
