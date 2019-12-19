/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BalanceSheetThresholdEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BalanceSheetThresholdEditController]);

    function BalanceSheetThresholdEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Balancesheet';
        vm.view = 'balancesheetthreshold-edit-view';
        vm.viewName = 'Balance Sheet Threshold';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.balanceSheetThreshold = {};

        vm.nonProducts = [];
        vm.runDate = {};
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var balancesheetthresholdRules = [];

        var setupRules = function () {
          
            balancesheetthresholdRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product is required" }
            }));

            balancesheetthresholdRules.push(new validator.PropertyRule("CaptionCode", {
                required: { message: "Caption is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.balancesheetthresholdId !== 0  ) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/balancesheetthreshold/getbalancesheetthreshold/' + $stateParams.balancesheetthresholdId, null,
                   function (result) {
                       vm.balanceSheetThreshold = result.data;
                       
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.balanceSheetThreshold = { ProductCode: '', CaptionCode:'', Rate: 0, Active: true };
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
            validator.ValidateModel(vm.balanceSheetThreshold, balancesheetthresholdRules);
            vm.viewModelHelper.modelIsValid = vm.balanceSheetThreshold.isValid;
            vm.viewModelHelper.modelErrors = vm.balanceSheetThreshold.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/balancesheetthreshold/updatebalancesheetthreshold', vm.balanceSheetThreshold,
               function (result) {
                   
                   $state.go('mpr-balancesheetthreshold-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.balanceSheetThreshold.errors;

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
                vm.viewModelHelper.apiPost('api/balancesheetthreshold/deletebalancesheetthreshold', vm.balanceSheetThreshold.BalanceSheetThresholdId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-balancesheetthreshold-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-balancesheetthreshold-list');
        };

        var getProducts = function () {
            vm.viewModelHelper.apiGet('api/mprproduct/getrealproducts', null,
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
