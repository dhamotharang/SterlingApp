/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExpenseRawBasisEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExpenseRawBasisEditController]);

    function ExpenseRawBasisEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'expenserawbasis-edit-view';
        vm.viewName = 'Expense Raw Basis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.expenseRawBasis = {};
        vm.basiscodes = [];
        vm.miscodes = [];
        
        //vm.Type = [
        //    { Id: 1, Name: 'GH' },
        //    { Id: 2, Name: 'HO' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        

        var expenserawbasisRules = [];

        var setupRules = function () {

            expenserawbasisRules.push(new validator.PropertyRule("BasisCode", {
                required: { message: "Basis Code is required" }
            }));

            expenserawbasisRules.push(new validator.PropertyRule("MISCode", {
                required: { message: "MIS Code is required" }
            }));

            expenserawbasisRules.push(new validator.PropertyRule("Weight", {
                required: { message: "Weight is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.expenserawbasisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/expenserawbasis/getexpenserawbasis/' + $stateParams.expenserawbasisId, null,
                   function (result) {
                       vm.expenseRawBasis = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.expenseRawBasis = { BasicCode: '', MISCode: '', Weight: 0, Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.expenseRawBasis, vm.expenseRawBasis);
            vm.viewModelHelper.modelIsValid = vm.expenseRawBasis.isValid;
            vm.viewModelHelper.modelErrors = vm.expenseRawBasis.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/expenserawbasis/updateexpenseRawBasis', vm.expenseRawBasis,
               function (result) {
                   
                   $state.go('mpr-expenserawbasis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.expenseRawBasis.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/expenserawbasis/deleteexpenserawbasis', vm.expenseRawBasis.ExpenseRawBasisId,//vm.expenseRawBasis.expenserawbasisId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-expenserawbasis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-expenserawbasis-list');
        };

        var intializeLookUp = function () {
            getBasisCodes();
            getMisCodes();
        }


        var getBasisCodes = function () {
            vm.viewModelHelper.apiGet('api/expensebasis/availableexpenseBasis', null,
                 function (result) {
                     vm.basiscodes = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load expense basis.', 'Fintrak');
                 }, null);
        }

        var getMisCodes = function () {
            vm.viewModelHelper.apiGet('api/costcentre/availablecostCentres', null,
                 function (result) {
                     vm.miscodes = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load cost centers.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
