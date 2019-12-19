/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NEABranchSharingRatioEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NEABranchSharingRatioEditController]);

    function NEABranchSharingRatioEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'neabranchsharingratio-edit-view';
        vm.viewName = 'NEA Branch Sharing Ratio';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.nEABranchSharingRatio = {};

        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var neabranchsharingratioRules = [];

        var setupRules = function () {

            neabranchsharingratioRules.push(new validator.PropertyRule("OwnerBranch", {
                required: { message: "Owner Branch Class is required" }
            }));

            neabranchsharingratioRules.push(new validator.PropertyRule("Beneficiary", {
                required: { message: "Beneficiary is required" }
            }));

            neabranchsharingratioRules.push(new validator.PropertyRule("Ratio", {
                required: { message: "Ratio is required" }
            }));

            neabranchsharingratioRules.push(new validator.PropertyRule("GL", {
                required: { message: "GL is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.neabranchsharingratioId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/neabranchsharingratio/getneabranchsharingratio/' + $stateParams.neabranchsharingratioId, null,
                   function (result) {
                       vm.nEABranchSharingRatio = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.nEABranchSharingRatio = { OwnerBranch: '', Beneficiary: '', Ratio: '', GL: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.nEABranchSharingRatio, neabranchsharingratioRules);
            vm.viewModelHelper.modelIsValid = vm.nEABranchSharingRatio.isValid;
            vm.viewModelHelper.modelErrors = vm.nEABranchSharingRatio.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/neabranchsharingratio/updateneabranchsharingratio', vm.nEABranchSharingRatio,
               function (result) {

                   $state.go('mpr-neabranchsharingratio-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.nEABranchSharingRatio.errors;

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
                vm.viewModelHelper.apiPost('api/neabranchsharingratio/deleteneabranchsharingratio', vm.nEABranchSharingRatio.NEABranchSharingRatioId,//vm.nEABranchSharingRatio.neabranchsharingratioId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-neabranchsharingratio-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-neabranchsharingratio-list');
        };

        setupRules();
        initialize();
    }
}());
