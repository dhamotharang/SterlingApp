/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NEASharingRatioFcyEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NEASharingRatioFcyEditController]);

    function NEASharingRatioFcyEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'neasharingratiofcy-edit-view';
        vm.viewName = 'NEA Sharing Ratio Fcy';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.nEASharingRatioFcy = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var neasharingratiofcyRules = [];

        var setupRules = function () {

            neasharingratiofcyRules.push(new validator.PropertyRule("Branch", {
                required: { message: "Branch is required" }
            }));
            neasharingratiofcyRules.push(new validator.PropertyRule("SBUCode", {
                required: { message: "SBUCode is required" }
            }));
            neasharingratiofcyRules.push(new validator.PropertyRule("Ratio", {
                required: { message: "Ratio is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.neasharingratiofcyId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/neasharingratiofcy/getneasharingratiofcy/' + $stateParams.neasharingratiofcyId, null,
                   function (result) {
                       vm.nEASharingRatioFcy = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.nEASharingRatioFcy = { Branch: '', SBUCode: '', Ratio: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.nEASharingRatioFcy, neasharingratiofcyRules);
            vm.viewModelHelper.modelIsValid = vm.nEASharingRatioFcy.isValid;
            vm.viewModelHelper.modelErrors = vm.nEASharingRatioFcy.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/neasharingratiofcy/updateneasharingratiofcy', vm.nEASharingRatioFcy,
               function (result) {

                   $state.go('mpr-neasharingratiofcy-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.nEASharingRatioFcy.errors;

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
                vm.viewModelHelper.apiPost('api/neasharingratiofcy/deleteneasharingratiofcy', vm.nEASharingRatioFcy.NEASharingRatioFcyId,//vm.nEASharingRatioFcy.neasharingratiofcyId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-neasharingratiofcy-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-neasharingratiofcy-list');
        };

        setupRules();
        initialize();
    }
}());
