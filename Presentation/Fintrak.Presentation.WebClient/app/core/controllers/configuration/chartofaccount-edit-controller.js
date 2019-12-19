/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ChartOfAccountEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ChartOfAccountEditController]);

    function ChartOfAccountEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'chartofaccount-edit-view';
        vm.viewName = 'Account';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.chartOfAccount = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.accountTypes = [
            { Id: 1, Name: 'View' },
            { Id: 2, Name: 'Asset' },
            { Id: 3, Name: 'Liability' },
            { Id: 4, Name: 'Income' },
            { Id: 5, Name: 'Expense' },
            { Id: 6, Name: 'ContigentAsset' },
            { Id: 7, Name: 'ContigentLiability' },
            { Id: 8, Name: 'Consolidation' }
        ];

        vm.accounts = [];
        vm.financialTypes = [];

        var chartofaccountRules = [];

        var setupRules = function () {
          
            chartofaccountRules.push(new validator.PropertyRule("AccountCode", {
                required: { message: "Code is required" }
            }));

            chartofaccountRules.push(new validator.PropertyRule("AccountName", {
                required: { message: "Name is required" }
            }));

            chartofaccountRules.push(new validator.PropertyRule("AccountType", {
                notZero: { message: "Account Type is required" }
            }));

            chartofaccountRules.push(new validator.PropertyRule("FinancialTypeId", {
                notZero: { message: "Financial Type is required" }
            }));

            chartofaccountRules.push(new validator.PropertyRule("Position", {
                notZero: { message: "Position is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.chartofaccountId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/chartofaccount/getchartofaccount/' + $stateParams.chartofaccountId, null,
                   function (result) {
                       vm.chartOfAccount = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.chartOfAccount = { AccountCode: '', AccountName: '',AccountType:1,FinancialTypeId:0,Position:1, Active: true };
            }
        }

        var intializeLookUp = function () {
            getAccounts();
            getFinancialTypes();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.chartOfAccount, chartofaccountRules);
            vm.viewModelHelper.modelIsValid = vm.chartOfAccount.isValid;
            vm.viewModelHelper.modelErrors = vm.chartOfAccount.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/chartofaccount/updatechartofaccount', vm.chartOfAccount,
               function (result) {
                   
                   $state.go('core-chartofaccount-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.chartOfAccount.errors;

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
                vm.viewModelHelper.apiPost('api/chartofaccount/deletechartofaccount', vm.chartOfAccount.ChartOfAccountId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-chartofaccount-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-chartofaccount-list');
        };

        var getAccounts = function () {
            vm.viewModelHelper.apiGet('api/chartofaccount/availablechartofaccounts', null,
                 function (result) {
                     vm.accounts = result.data;
                  
                 },
                 function (result) {

                 }, null);
        }

        var getFinancialTypes = function () {
            vm.viewModelHelper.apiGet('api/financialtype/availablefinancialTypes', null,
                 function (result) {
                     vm.financialTypes = result.data;

                 },
                 function (result) {

                 }, null);
        }
       
        setupRules();
        initialize(); 
    }
}());
