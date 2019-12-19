/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MerchantEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MerchantEditController]);

    function MerchantEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CDQM';
        vm.view = 'merchant-edit-view';
        vm.viewName = 'Merchant';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.merchant = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var merchantRules = [];

        var setupRules = function () {
          
            
            //merchantRules.push(new validator.PropertyRule("StreetName", {
            //    required: { message: "Street Name is required" }
            //}));

          

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.merchantId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/cdqmmerchant/getcdqmmerchant/' + $stateParams.merchantId, null,
                   function (result) {
                       vm.merchant = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.merchant = { Valid: '', Invalid: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.merchant, merchantRules);
            vm.viewModelHelper.modelIsValid = vm.merchant.isValid;
            vm.viewModelHelper.modelErrors = vm.merchant.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/cdqmmerchant/updatecdqmmerchant', vm.merchant,
               function (result) {
                   
                   $state.go('cdqm-merchant-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.merchant.errors;

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
                vm.viewModelHelper.apiPost('api/cdqmmerchant/deletecdqmmerchant', vm.merchant.MerchantId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('cdqm-merchant-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('cdqm-merchant-list');
        };

        setupRules();
        initialize(); 
    }
}());
