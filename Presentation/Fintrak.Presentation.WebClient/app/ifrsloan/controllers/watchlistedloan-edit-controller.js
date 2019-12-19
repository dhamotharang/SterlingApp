/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("WatchListedLoanEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        WatchListedLoanEditController]);

    function WatchListedLoanEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'watchlistedloan-edit-view';
        vm.viewName = 'WatchListed Loans';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.watchListedLoans = {};   
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var watchlistedloanRules = [];

        var setupRules = function () {

            watchlistedloanRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));    
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               //intializeLookUp();

                if ($stateParams.watchlistedloanId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/watchlistedloan/getwatchlistedloan/' + $stateParams.watchlistedloanId, null,
                   function (result) {
                       vm.watchListedLoans = result.data;
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.watchListedLoans = { RefNo: '', CompanyCode: '', Active: true };
            }
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.watchListedLoans, watchlistedloanRules);
            vm.viewModelHelper.modelIsValid = vm.watchListedLoans.isValid;
            vm.viewModelHelper.modelErrors = vm.watchListedLoans.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/watchlistedloan/updatewatchlistedloan', vm.watchListedLoans,
               function (result) {
                   
                   $state.go('ifrsloan-watchlistedloan-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.watchListedLoans.errors;

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
                vm.viewModelHelper.apiPost('api/watchlistedloan/deletewatchlistedloan', vm.watchListedLoans.WatchListedLoanId,//vm.watchListedLoan.watchlistedloanId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-watchlistedloan-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-watchlistedloan-list');
        };
        setupRules();
        initialize();
    }
}());
