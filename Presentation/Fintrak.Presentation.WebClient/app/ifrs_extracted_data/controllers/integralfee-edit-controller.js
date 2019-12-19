/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IntegralFeeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IntegralFeeEditController]);

    function IntegralFeeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IntegralFee';
        vm.view = 'integralfee-edit-view';
        vm.viewName = 'Integral Fee';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.integralFee = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

       vm.openedRunDate = false;
 
        var integralFeeRules = [];

        var setupRules = function () {
          
            integralFeeRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            integralFeeRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            integralFeeRules.push(new validator.PropertyRule("Date", {
                notZero: { message: "Date is required" }
            }));

            integralFeeRules.push(new validator.PropertyRule("FeeAmount", {
                required: { message: "FeeAmount is required" }
            }));

            integralFeeRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.integralFeeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/integralfee/getintegralfee/' + $stateParams.integralFeeId, null,
                   function (result) {
                       vm.integralFee = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.integralFee = { RefNo: '', AccountNo: '', Date: '', FeeAmount: '', Description: '', CompanyCode: '', Active: true };
            }
        }


        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.integralFee, integralFeeRules);
            vm.viewModelHelper.modelIsValid = vm.integralFee.isValid;
            vm.viewModelHelper.modelErrors = vm.integralFee.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/integralfee/updateintegralfee', vm.integralFee,
               function (result) {
                   
                   $state.go('ifrs-integralfee-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.integralFee.errors;

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
                vm.viewModelHelper.apiPost('api/integralfee/deleteintegralfee', vm.integralFee.IntegralFeeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-integralfee-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-integralfee-list');
        };
 
         vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
         }

        setupRules();
        initialize(); 
    }
}());
