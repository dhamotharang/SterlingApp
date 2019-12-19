/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRBalanceSheetAdjustmentEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MPRBalanceSheetAdjustmentEditController]);

    function MPRBalanceSheetAdjustmentEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR BS';
        vm.view = 'mprbalancesheetadjustment-edit-view';
        vm.viewName = 'BalanceSheet Adjustment Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprBalancesheetAdjustment = {};
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var mprbalancesheetadjustmentRules = [];

        var setupRules = function () {

            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team Code is required" }
            }));

            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "Account Officer Code is required" }
            }));

            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("ProductCode", {
                required: { message: "Product Code is required" }
            }));

            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("Category", {
                required: { message: "Category is required" }
            }));

            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("CurrencyType", {
                required: { message: "Currency Type is required" }
            }));
            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "Account No is required" }
            }));

            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("AccountName", {
                required: { message: "Account Name is required" }
            }));
            mprbalancesheetadjustmentRules.push(new validator.PropertyRule("Rundate", {
                required: { message: "RunDate is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               // intializeLookUp();

                if ($stateParams.mprbalancesheetadjustmentId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/mprbalancesheetadjustment/getmprbalancesheetadjustment/' + $stateParams.mprbalancesheetadjustmentId, null,
                   function (result) {
                       vm.mprBalancesheetAdjustment = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.mprBalancesheetAdjustment = { TeamCode: '', AccountOfficerCode: '',   RunDate: '' };
            }
        }
        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.mprBalancesheetAdjustment, mprbalancesheetadjustmentRules);
            vm.viewModelHelper.modelIsValid = vm.mprBalancesheetAdjustment.isValid;
            vm.viewModelHelper.modelErrors = vm.mprBalancesheetAdjustment.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/mprbalancesheetadjustment/updatemprbalancesheetadjustment', vm.mprBalancesheetAdjustment,
               function (result) {
                   
                   $state.go('mpr-mprbalancesheetadjustment-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.mprBalancesheetAdjustment.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
            vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/mprbalancesheetadjustment/deletemprbalancesheetadjustment', + $stateParams.mprbalancesheetadjustmentId , //
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-mprbalancesheetadjustment-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-mprbalancesheetadjustment-list');
        };

        setupRules();
        initialize();
    }
}());
