/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexBusinessRuleEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexBusinessRuleEditController]);

    function OpexBusinessRuleEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexbusinessrule-edit-view';
        vm.viewName = 'Opex Business Rule';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexBusinessRule = {};
       
        vm.Type = [
            { Id: 1, Name: 'GROUP HEAD' },
            { Id: 2, Name: 'ALLOCATED EXPENSE' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexbusinessruleRules = [];

        var setupRules = function () {

            opexbusinessruleRules.push(new validator.PropertyRule("Source", {
                required: { message: "Source is required" }
            }));

            opexbusinessruleRules.push(new validator.PropertyRule("Target", {
                required: { message: "Target is required" }
            }));

            opexbusinessruleRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

            opexbusinessruleRules.push(new validator.PropertyRule("Ratio", {
                required: { message: "Ratio is required" }
            }));

            opexbusinessruleRules.push(new validator.PropertyRule("Template", {
                required: { message: "Template is required" }
            }));
            opexbusinessruleRules.push(new validator.PropertyRule("Position", {
                required: { message: "Position is required" }
            }));
            opexbusinessruleRules.push(new validator.PropertyRule("Total", {
                required: { message: "Total is required" }
            }));
                   
            opexbusinessruleRules.push(new validator.PropertyRule("Type", {
                required: { message: "Type is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               // intializeLookUp();

                if ($stateParams.opexbusinessruleId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexbusinessrule/getopexbusinessrule/' + $stateParams.opexbusinessruleId, null,
                   function (result) {
                       vm.opexBusinessRule = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.opexBusinessRule = { Source: '',BasisCaption: '' ,Target: '',Description: '',Ratio: '' ,Template: '',Position:0,Total:'',Type:'', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexBusinessRule, opexbusinessruleRules);
            vm.viewModelHelper.modelIsValid = vm.opexBusinessRule.isValid;
            vm.viewModelHelper.modelErrors = vm.opexBusinessRule.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexbusinessrule/updateopexbusinessrule', vm.opexBusinessRule,
               function (result) {
                   
                   $state.go('mpr-opexbusinessrule-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexBusinessRule.errors;

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
                vm.viewModelHelper.apiPost('api/opexbusinessrule/deleteopexbusinessrule', vm.opexBusinessRule.OpexBusinessRuleId,//vm.opexBusinessRule.opexbusinessruleId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexbusinessrule-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-opexbusinessrule-list');
        };

        setupRules();
        initialize();
    }
}());
