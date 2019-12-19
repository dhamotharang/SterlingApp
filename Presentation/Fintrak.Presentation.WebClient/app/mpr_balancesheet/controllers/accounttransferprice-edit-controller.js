/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AccountTransferPriceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        AccountTransferPriceEditController]);

    function AccountTransferPriceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'accounttransferprice-edit-view';
        vm.viewName = 'Account Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.accountTransferPrice = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.category = [
         { Id: 2, Name: 'Asset' },
         { Id: 3, Name: 'Liability' }
        ];

        var accounttransferpriceRules = [];

        vm.solutions = [];

        var setupRules = function () {

            accounttransferpriceRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo Code is required" }
            }));
            accounttransferpriceRules.push(new validator.PropertyRule("Category", {
                required: { message: "Category is required" }
            }));
            accounttransferpriceRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));
            accounttransferpriceRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));
            accounttransferpriceRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));
            accounttransferpriceRules.push(new validator.PropertyRule("SolutionId", {
                required: { message: "Solution is required" }
            }));
        }



        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                 intializeLookUp();

                if ($stateParams.accounttransferpriceId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/accounttransferprice/getaccounttransferprice/' + $stateParams.accounttransferpriceId, null,
                   function (result) {
                       vm.accountTransferPrice = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {

                   }, null);
                }
                else
                    vm.accountTransferPrice = { AccountNo: '', Category: 2, Rate: 0, Year: 0, Period: 0, SolutionId: '', Active: true };
            }
        }

        var initialView = function () {

        }

        var intializeLookUp = function () {
            getSolutions();
            // getLoanProducts();
            //getScheduleTypes();
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.accountTransferPrice, accounttransferpriceRules);
            vm.viewModelHelper.modelIsValid = vm.accountTransferPrice.isValid;
            vm.viewModelHelper.modelErrors = vm.accountTransferPrice.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/accounttransferprice/updateaccounttransferprice', vm.accountTransferPrice,
               function (result) {
                   //
                   $state.go('mpr-accounttransferprice-list');
               },
               function (result) {

               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.accountTransferPrice.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/accounttransferprice/deleteaccounttransferprice', vm.accountTransferPrice.AccountTransferPriceId,//vm.accountTransferPrice.accounttransferpriceId,
              function (result) {
                  //
                  $state.go('mpr-accounttransferprice-list');
              },
              function (result) {

              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-accounttransferprice-list');
        };


        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                 },
                 function (result) {

                 }, null);
        }


        setupRules();
        initialize();
    }
}());
