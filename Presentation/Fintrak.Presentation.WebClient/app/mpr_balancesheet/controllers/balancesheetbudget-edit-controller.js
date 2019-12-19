/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BalanceSheetBudgetEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BalanceSheetBudgetEditController]);

    function BalanceSheetBudgetEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Balancesheet';
        vm.view = 'balancesheetbudget-edit-view';
        vm.viewName = 'Balance Sheet Budget';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.balanceSheetBudget = {};

        vm.nonProducts = [];
        vm.runDate = {};
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var balancesheetbudgetRules = [];

        var setupRules = function () {
          
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.balancesheetbudgetId !== 0  ) {
                    vm.showChildren = true;

                    var url = '';
                    if ($stateParams.budgettype === 2)
                        url = 'api/balancesheetbudgetofficer/getbalancesheetbudgetofficer';
                    else
                        url = 'api/balancesheetbudget/getbalancesheetbudget';

                    vm.viewModelHelper.apiGet(url + '/' + $stateParams.budgetId, null,
                   function (result) {
                       vm.balanceSheetBudget = result.data;
                       
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.balanceSheetBudget = { CaptionName: '', MisCode: '', Year: '', CompanyCode: '', Mth1: 0, Mth2:0, Mth3:0, Mth4:0, Mth5:0, Mth6:0, Mth7:0, Mth8:0, Mth9:0, Mth10:0, Mth11:0, Mth12:0, Active: true };
            }
        }

        var intializeLookUp = function () {
          
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            var url = '';
            if ($stateParams.budgettype === 2)
                url = 'api/balancesheetbudgetofficer/updatebalancesheetbudgetofficer';
            else
                url = 'api/balancesheetbudget/updatebalancesheetbudget';

            //Validate
            validator.ValidateModel(vm.balanceSheetBudget, balancesheetbudgetRules);
            vm.viewModelHelper.modelIsValid = vm.balanceSheetBudget.isValid;
            vm.viewModelHelper.modelErrors = vm.balanceSheetBudget.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost(url, vm.balanceSheetBudget,
               function (result) {
                   
                   $state.go('mpr-balancesheetbudget-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.balanceSheetBudget.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
                toastr.error(errorList, 'Fintrak');

            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );
            var url = '';
            if ($stateParams.budgettype === 2)
                url = 'api/balancesheetbudgetofficer/deletebalancesheetbudgetofficer';
            else
                url = 'api/balancesheetbudget/deletebalancesheetbudget';

            if (deleteFlag) {
                vm.viewModelHelper.apiPost(url, vm.balanceSheetBudget.BudgetId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-balancesheetbudget-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-balancesheetbudget-list');
        };

        setupRules();
        initialize(); 
    }
}());
