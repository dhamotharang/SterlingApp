/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexRawExpenseEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexRawExpenseEditController]);

    function OpexRawExpenseEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexrawexpense-edit-view';
        vm.viewName = 'Opex Raw Expense';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexRawExpenses = {};
        //vm.parents = [];
        //vm.CCDefintions = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexrawexpenseRules = [];

        var setupRules = function () {

            opexrawexpenseRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GL Code is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("GLName", {
                required: { message: "GL Name is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("PostDate", {
                required: { message: "Post Date is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("CheckMisCode", {
                required: { message: "Check MIS Code is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "MIS Code is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("BranchCode", {
                required: { message: "Branch Code is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("TranID", {
                required: { message: "TranID is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("SubGLCode", {
                required: { message: "Sub GL Code is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("DR", {
                required: { message: "DR is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("CR", {
                required: { message: "CR is required" }
            }));

            opexrawexpenseRules.push(new validator.PropertyRule("Narrative", {
                required: { message: "Narrative is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.opexrawexpenseId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexrawexpense/getopexRawExpense/' + $stateParams.opexrawexpenseId, null,
                   function (result) {
                       vm.opexRawExpense = result.data;
                       //getCCDefintion();
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.opexRawExpense = {
                        GLCode: '', GLName: '', PostDate: 0,
                        Amount: '', Description: '', CheckMisCode: '', MisCode: '',
                        BranchCode: '', TranID: '', SubGLCode: '', DR: '', CR: '',
                        Narrative: '', Active: true
                    };
            }
        }


        var intializeLookUp = function () {          
        //    getName();
        //    getCCDefintion();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexRawExpense, opexrawexpenseRules);
            vm.viewModelHelper.modelIsValid = vm.opexRawExpense.isValid;
            vm.viewModelHelper.modelErrors = vm.opexRawExpense.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexrawexpense/updateopexRawExpense', vm.opexRawExpense,
               function (result) {
                   
                   $state.go('mpr-opexrawexpense-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexRawExpense.errors;

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
                vm.viewModelHelper.apiPost('api/opexrawexpense/deleteopexRawExpense', vm.opexRawExpense.CostCentreId,//vm.opexRawExpense.opexrawexpenseId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexrawexpense-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-opexrawexpense-list');
        };

        setupRules();
        initialize();
    }
}());
