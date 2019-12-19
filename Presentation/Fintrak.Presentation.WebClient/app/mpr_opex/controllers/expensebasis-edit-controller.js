/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ExpenseBasisEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ExpenseBasisEditController]);

    function ExpenseBasisEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'expensebasis-edit-view';
        vm.viewName = 'Expense Basis';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.expenseBasis = {};

        vm.valueTypes = [
            { Id: 1, Name: 'Actual' },
            { Id: 2, Name: 'Average' }
        ];

        vm.itemTypes = [
           { Id: 1, Name: 'Product' },
           { Id: 2, Name: 'GL' },
           { Id: 3, Name: 'Team' }
        ];

        vm.categories = [
           { Id: 1, Name: 'Ledger Cost Allocation' },
           { Id: 2, Name: 'SBU Cost Allocation' },
           { Id: 3, Name: 'Both' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var expensebasisRules = [];

        var setupRules = function () {

            expensebasisRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            expensebasisRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.expensebasisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/expensebasis/getexpensebasis/' + $stateParams.expensebasisId, null,
                   function (result) {
                       vm.expenseBasis = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.expenseBasis = { Code: '', Name: '',Category:1, ValueType: 1, ItemType: 1, Active: true };
            }
        }

        var intializeLookUp = function () {
            getTeamDefinitions();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.expenseBasis, vm.expenseBasis);
            vm.viewModelHelper.modelIsValid = vm.expenseBasis.isValid;
            vm.viewModelHelper.modelErrors = vm.expenseBasis.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/expensebasis/updateexpensebasis', vm.expenseBasis,
               function (result) {
                   
                   $state.go('mpr-expensebasis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.expenseBasis.errors;

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
                vm.viewModelHelper.apiPost('api/expensebasis/deleteexpensebasis', vm.expenseBasis.ExpenseBasisId,//vm.expenseBasis.expensebasisId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-expensebasis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-expensebasis-list');
        };

        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                 function (result) {
                     vm.teamDefinitions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
