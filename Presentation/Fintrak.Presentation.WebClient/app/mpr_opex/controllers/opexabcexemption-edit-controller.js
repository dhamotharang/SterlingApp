/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexAbcExemptionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexAbcExemptionEditController]);

    function OpexAbcExemptionEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexabcexemption-edit-view';
        vm.viewName = 'Opex ABC Exemption';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexAbcExemption = {};
        vm.companies = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexabcexemptionRules = [];

        var setupRules = function () {

            opexabcexemptionRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "MisCode is required" }
            }));

            opexabcexemptionRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "Company Name is required" }
            }));
                      
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                
                if ($stateParams.opexabcexemptionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexabcexemption/getopexabcexemption/' + $stateParams.opexabcexemptionId, null,
                   function (result) {
                       vm.opexAbcExemption = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.opexAbcExemption = { MisCode: '', CompanyCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
          getCompanies();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexAbcExemption, opexabcexemptionRules);
            vm.viewModelHelper.modelIsValid = vm.opexAbcExemption.isValid;
            vm.viewModelHelper.modelErrors = vm.opexAbcExemption.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexabcexemption/updateopexabcexemption', vm.opexAbcExemption,
               function (result) {
                   
                   $state.go('mpr-opexabcexemption-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexAbcExemption.errors;

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
                vm.viewModelHelper.apiPost('api/opexabcexemption/deleteopexabcexemption', vm.opexAbcExemption.OpexAbcExemptionId,//vm.opexAbcExemption.opexabcexemptionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexabcexemption-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-opexabcexemption-list');
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
