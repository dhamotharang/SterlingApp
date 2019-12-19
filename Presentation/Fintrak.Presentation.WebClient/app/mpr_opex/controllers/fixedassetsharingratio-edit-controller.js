/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FixedAssetSharingRatioEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FixedAssetSharingRatioEditController]);

    function FixedAssetSharingRatioEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'fixedassetsharingratio-edit-view';
        vm.viewName = 'Fixed Asset Sharing Ratio';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.fixedAssetSharingRatio = {};
       
        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var fixedassetsharingratioRules = [];

        var setupRules = function () {

            fixedassetsharingratioRules.push(new validator.PropertyRule("Branch", {
                required: { message: "Branch is required" }
            }));

            fixedassetsharingratioRules.push(new validator.PropertyRule("SBUCode", {
                required: { message: "SBU Code is required" }
            }));

            fixedassetsharingratioRules.push(new validator.PropertyRule("Ratio", {
                required: { message: "Ratio is required" }
            }));
                     
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               // intializeLookUp();

                if ($stateParams.fixedAssetSharingRatioId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/fixedassetsharingratio/getfixedAssetSharingRatio/' + $stateParams.fixedAssetSharingRatioId, null,
                   function (result) {
                       vm.fixedAssetSharingRatio = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.fixedAssetSharingRatio = { Branch: '',Ratio: '', SBUCode:'', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.fixedAssetSharingRatio, fixedassetsharingratioRules);
            vm.viewModelHelper.modelIsValid = vm.fixedAssetSharingRatio.isValid;
            vm.viewModelHelper.modelErrors = vm.fixedAssetSharingRatio.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/fixedassetsharingratio/updatefixedassetsharingratio', vm.fixedAssetSharingRatio,
               function (result) {
                   
                   $state.go('mpr-fixedassetsharingratio-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.fixedAssetSharingRatio.errors;

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
                vm.viewModelHelper.apiPost('api/fixedassetsharingratio/deletefixedassetsharingratio', vm.fixedAssetSharingRatio.FixedAssetSharingRatioId,//vm.fixedAssetSharingRatio.fixedassetsharingratioId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-fixedassetsharingratio-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-fixedassetsharingratio-list');
        };

        setupRules();
        initialize();
    }
}());
