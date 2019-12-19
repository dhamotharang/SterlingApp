/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CategoryTransferPriceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CategoryTransferPriceEditController]);

    function CategoryTransferPriceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'categorytransferprice-edit-view';
        vm.viewName = 'Category Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.categoryTransferPrice = {};

        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var categorytransferpriceRules = [];

        var setupRules = function () {

            categorytransferpriceRules.push(new validator.PropertyRule("Category", {
                required: { message: "Category is required" }
            }));
            categorytransferpriceRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));
            categorytransferpriceRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));
            categorytransferpriceRules.push(new validator.PropertyRule("CurrencyType", {
                required: { message: "CurrencyType is required" }
            }));
            categorytransferpriceRules.push(new validator.PropertyRule("Rate", {
                required: { message: "Rate is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.categorytransferpriceId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/categorytransferprice/getcategorytransferprice/' + $stateParams.categorytransferpriceId, null,
                   function (result) {
                       vm.categoryTransferPrice = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.categoryTransferPrice = { Category: '', Period: '', Year: '', CurrencyType: '', Rate: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.categoryTransferPrice, categorytransferpriceRules);
            vm.viewModelHelper.modelIsValid = vm.categoryTransferPrice.isValid;
            vm.viewModelHelper.modelErrors = vm.categoryTransferPrice.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/categorytransferprice/updatecategorytransferprice', vm.categoryTransferPrice,
               function (result) {

                   $state.go('mpr-categorytransferprice-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.categoryTransferPrice.errors;

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
                vm.viewModelHelper.apiPost('api/categorytransferprice/deletecategorytransferprice', vm.categoryTransferPrice.CategoryTransferPriceId,//vm.categoryTransferPrice.categorytransferpriceId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-categorytransferprice-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-categorytransferprice-list');
        };

        setupRules();
        initialize();
    }
}());
