/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RevenueEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RevenueEditController]);

    function RevenueEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'revenue-edit-view';
        vm.viewName = 'Revenue';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.revenue = {};
        vm.companies = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var revenueRules = [];

        var setupRules = function () {

            revenueRules.push(new validator.PropertyRule("Narrative", {
                required: { message: "Narration is required" }
            }));

            revenueRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team Code is required" }
            }));

            revenueRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "Account Officer Code is required" }
            }));

            revenueRules.push(new validator.PropertyRule("RelatedAccount", {
                required: { message: "Related Account is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.revenueId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/revenue/getrevenue/' + $stateParams.revenueId, null,
                   function (result) {
                       vm.revenue = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.revenue = { Narrative: '', TeamCode: '', AccountOfficerCode: '', RelatedAccount: '', Active: true };
            }
        }

        var intializeLookUp = function () {
          getCompanies();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.revenue, revenueRules);
            vm.viewModelHelper.modelIsValid = vm.revenue.isValid;
            vm.viewModelHelper.modelErrors = vm.revenue.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/revenue/updaterevenue', vm.revenue,
               function (result) {
                   
                   $state.go('mpr-revenue-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.revenue.errors;

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
                vm.viewModelHelper.apiPost('api/revenue/deleterevenue', vm.revenue.revenueId,//vm.revenue.revenueId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-revenue-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-revenue-list');
        };

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        setupRules();
        initialize();
    }
}());
