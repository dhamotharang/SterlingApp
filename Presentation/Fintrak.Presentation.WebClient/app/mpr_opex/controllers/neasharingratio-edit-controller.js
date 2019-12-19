/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NEASharingRatioEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NEASharingRatioEditController]);

    function NEASharingRatioEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'neasharingratio-edit-view';
        vm.viewName = 'NEA Sharing Ratio';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.nEASharingRatio = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var neasharingratioRules = [];

        var setupRules = function () {

            neasharingratioRules.push(new validator.PropertyRule("Branch", {
                required: { message: "Branch is required" }
            }));
            neasharingratioRules.push(new validator.PropertyRule("SBUCode", {
                required: { message: "SBUCode is required" }
            }));
            neasharingratioRules.push(new validator.PropertyRule("Ratio", {
                required: { message: "Ratio is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.neasharingratioId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/neasharingratio/getneasharingratio/' + $stateParams.neasharingratioId, null,
                   function (result) {
                       vm.nEASharingRatio = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.nEASharingRatio = { Branch: '', SBUCode: '', Ratio: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.nEASharingRatio, neasharingratioRules);
            vm.viewModelHelper.modelIsValid = vm.nEASharingRatio.isValid;
            vm.viewModelHelper.modelErrors = vm.nEASharingRatio.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/neasharingratio/updateneasharingratio', vm.nEASharingRatio,
               function (result) {

                   $state.go('mpr-neasharingratio-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.nEASharingRatio.errors;

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
                vm.viewModelHelper.apiPost('api/neasharingratio/deleteneasharingratio', vm.nEASharingRatio.NEASharingRatioId,//vm.nEASharingRatio.neasharingratioId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-neasharingratio-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-neasharingratio-list');
        };

        setupRules();
        initialize();
    }
}());
