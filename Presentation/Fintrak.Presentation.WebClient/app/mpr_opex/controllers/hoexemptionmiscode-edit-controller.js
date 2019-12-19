/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("HoExemptionMISCodeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        HoExemptionMISCodeEditController]);

    function HoExemptionMISCodeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'hoexemptionmiscode-edit-view';
        vm.viewName = 'Ho Exemption MISCode';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.hoExemptionMISCode = {};
       
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var hoexemptionmiscodeRules = [];

        var setupRules = function () {

            hoexemptionmiscodeRules.push(new validator.PropertyRule("MIS_Code", {
                required: { message: "MIS CODE is required" }
            }));
             
                     
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               // intializeLookUp();

                if ($stateParams.id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/hoexemptionmiscode/gethoexemptionmiscode/' + $stateParams.id, null,
                   function (result) {
                       vm.hoExemptionMISCode = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.hoExemptionMISCode = { MIS_Code: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.hoExemptionMISCode, hoexemptionmiscodeRules);
            vm.viewModelHelper.modelIsValid = vm.hoExemptionMISCode.isValid;
            vm.viewModelHelper.modelErrors = vm.hoExemptionMISCode.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/hoexemptionmiscode/updatehoexemptionmiscode', vm.hoExemptionMISCode,
               function (result) {
                   
                   $state.go('mpr-hoexemptionmiscode-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.hoExemptionMISCode.errors;

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
                vm.viewModelHelper.apiPost('api/hoexemptionmiscode/deletehoexemptionmiscode', vm.hoExemptionMISCode.Id,//vm.hoExemptionMISCode.hoexemptionmiscodeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-hoexemptionmiscode-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-hoexemptionmiscode-list');
        };

        setupRules();
        initialize();
    }
}());
