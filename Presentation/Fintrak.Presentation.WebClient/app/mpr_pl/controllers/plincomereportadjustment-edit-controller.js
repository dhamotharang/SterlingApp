/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PLIncomeReportAdjustmentEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PLIncomeReportAdjustmentEditController]);

    function PLIncomeReportAdjustmentEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'plincomereportadjustment-edit-view';
        vm.viewName = 'PL Income Report Adjustment';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.plIncomeReportAdjustment = {};
        vm.companies = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var plincomereportadjustmentRules = [];

        var setupRules = function () {

            plincomereportadjustmentRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team Code is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "Account Officer Code is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("Narrative", {
                required: { message: "Narrative is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("BranchCode", {
                required: { message: "Branch Code is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("GLCode", {
                required: { message: "GL Code is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("RelatedAccount", {
                required: { message: "Related Account is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));

            plincomereportadjustmentRules.push(new validator.PropertyRule("RunDate", {
                required: { message: "RunDate is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.plincomereportadjustmentId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/plincomereportadjustment/getplincomereportadjustment/' + $stateParams.plincomereportadjustmentId, null,
                   function (result) {
                       vm.plIncomeReportAdjustment = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.plIncomeReportAdjustment = { TeamCode: '', AccountOfficerCode: '', Narrative: '', BranchCode: '', GLCode: '', Caption: '', RelatedAccount: '', Amount: '', RunDate: '' };
            }
        }

        var intializeLookUp = function () {
          getCompanies();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.plIncomeReportAdjustment, plincomereportadjustmentRules);
            vm.viewModelHelper.modelIsValid = vm.plIncomeReportAdjustment.isValid;
            vm.viewModelHelper.modelErrors = vm.plIncomeReportAdjustment.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/plincomereportadjustment/updateplincomereportadjustment', vm.plIncomeReportAdjustment,
               function (result) {
                   
                   $state.go('mpr-plincomereportadjustment-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.plIncomeReportAdjustment.errors;

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
                vm.viewModelHelper.apiPost('api/plincomereportadjustment/deleteplincomereportadjustment', vm.plIncomeReportAdjustment.Id,//vm.plIncomeReportAdjustment.plincomereportadjustmentId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-plincomereportadjustment-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-plincomereportadjustment-list');
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
