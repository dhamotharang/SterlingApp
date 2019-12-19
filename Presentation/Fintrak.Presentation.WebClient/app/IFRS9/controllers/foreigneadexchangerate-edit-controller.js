/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ForeignEADExchangeRateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ForeignEADExchangeRateEditController]);

    function ForeignEADExchangeRateEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'foreigneadexchangerate-edit-view';
        vm.viewName = 'Foreign EAD Exchange Rates Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        vm.foreigneadexchangerate = {};
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        vm.currencies = [];
        vm.status =  false;

        var foreigneadexchangerateRules = [];
   

        var setupRules = function () {

            foreigneadexchangerateRules.push(new validator.PropertyRule("Currency", {
                required: { message: "Currency is required" }
            }));

            foreigneadexchangerateRules.push(new validator.PropertyRule("InterestRate", {
                required: { message: "Exchange Rate is required" }
            }));

            foreigneadexchangerateRules.push(new validator.PropertyRule("IntRate_date", {
                required: { message: "Date is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.foreigneadexchangerateId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/foreigneadexchangerate/getforeigneadexchangerate/' + $stateParams.foreigneadexchangerateId, null,
                   function (result) {
                       vm.foreigneadexchangerate = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                  //  alert(vm.PrevctlrStatus);
                    vm.foreigneadexchangerate = { Active: true };
            }
        }

        var intializeLookUp = function () {
            getCurrencies();
             }

        var initialView = function () {

        }
       

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.foreigneadexchangerate, foreigneadexchangerateRules);
            vm.viewModelHelper.modelIsValid = vm.foreigneadexchangerate.isValid;
            vm.viewModelHelper.modelErrors = vm.foreigneadexchangerate.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/foreigneadexchangerate/updateforeigneadexchangerate', vm.foreigneadexchangerate,
               function (result) {

                   $state.go('ifrs9-foreigneadexchangerate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.foreigneadexchangerate.errors;

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
                vm.viewModelHelper.apiPost('api/foreigneadexchangerate/deleteforeigneadexchangerate', vm.foreigneadexchangerate.ForeignEADExchangeRateId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-foreigneadexchangerate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrs9-foreigneadexchangerate-list');
        };

        var getCurrencies = function () {
            vm.viewModelHelper.apiGet('api/currency/availablecurrencies', null,
                 function (result) {
                     vm.currencies = result.data;
                     vm.init === true;

                     angular.forEach(vm.currencies, function (currency) {
                         vm.firstCurrency = currency.Symbol;
                        // vm.foreigneadexchangerate.Currency = currency.Symbol;
                     });


                 },
                 function (result) {
                     toastr.error('Unable to load currencies', 'Fintrak');
                 }, null);
        }
        setupRules();
        initialize();
    }
}());
