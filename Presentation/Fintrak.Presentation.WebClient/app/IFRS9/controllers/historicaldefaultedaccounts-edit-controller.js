/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("HistoricalDefaultedAccountsEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        HistoricalDefaultedAccountsEditController]);

    function HistoricalDefaultedAccountsEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'historicaldefaultedaccounts-edit-view';
        vm.viewName = 'Historical Defaulted Accounts';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.historicalDefaultedAccounts = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var historicaldefaultedaccountsRules = [];


        var setupRules = function () {
          
            historicaldefaultedaccountsRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("Sector", {
                required: { message: "Sector is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("ODLimit", {
                required: { message: "ODLimit is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("PrincipalOutstandingBal", {
                required: { message: "PrincipalOutstandingBal is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("BalanceOnRefDate", {
                required: { message: "BalanceOnRefDate is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("BalanceOnDefaultDate", {
                required: { message: "BalanceOnDefaultDate is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("Currency", {
                required: { message: "Currency is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("ExchangeRate", {
                required: { message: "ExchangeRate is required" }
            }));

            //historicaldefaultedaccountsRules.push(new validator.PropertyRule("ValueDate", {
            //    required: { message: "ValueDate is required" }
            //}));

            //historicaldefaultedaccountsRules.push(new validator.PropertyRule("MaturityDate", {
            //    required: { message: "MaturityDate is required" }
            //}));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("RunDate", {
                required: { message: "RunDate is required" }
            }));

            historicaldefaultedaccountsRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.defaultedAccountId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/historicaldefaultedaccounts/gethistoricaldefaultedaccount/' + $stateParams.defaultedAccountId, null,
                   function (result) {
                       vm.historicalDefaultedAccounts = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.historicalDefaultedAccounts = { RefNo: '', Sector: '', ODLimit: '', PrincipalOutstandingBal: '', BalanceOnRefDate: '', BalanceOnDefaultDate: '', Currency: '', ExchangeRate: '', RunDate: '', Classification: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.historicalDefaultedAccounts, historicaldefaultedaccountsRules);
            vm.viewModelHelper.modelIsValid = vm.historicalDefaultedAccounts.isValid;
            vm.viewModelHelper.modelErrors = vm.historicalDefaultedAccounts.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/historicaldefaultedaccounts/updatehistoricaldefaultedaccount', vm.historicalDefaultedAccounts,
               function (result) {
                   
                   $state.go('ifrs9-historicaldefaultedaccounts-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.historicalDefaultedAccounts.errors;

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
                vm.viewModelHelper.apiPost('api/historicaldefaultedaccounts/deletehistoricaldefaultedaccount', vm.historicalDefaultedAccounts.DefaultedAccountId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-historicaldefaultedaccounts-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-historicaldefaultedaccounts-list');
        };

        setupRules();
        initialize(); 
    }
}());
