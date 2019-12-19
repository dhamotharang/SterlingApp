/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LowCostRemapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LowCostRemapEditController]);

    function LowCostRemapEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'lowcostremap-edit-view';
        vm.viewName = 'Low Cost Remap';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.lowCostRemap = {};

        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var lowcostremapRules = [];

        var setupRules = function () {

            lowcostremapRules.push(new validator.PropertyRule("LowCostItem", {
                required: { message: "LowCostItem is required" }
            }));
            lowcostremapRules.push(new validator.PropertyRule("Remmaped", {
                required: { message: "Remmaped is required" }
            }));
            lowcostremapRules.push(new validator.PropertyRule("FreqLevel", {
                required: { message: "FreqLevel is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.lowcostremapId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/lowcostremap/getlowcostremap/' + $stateParams.lowcostremapId, null,
                   function (result) {
                       vm.lowCostRemap = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.lowCostRemap = { LowCostItem: '', Remmaped: '', FreqLevel: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.lowCostRemap, lowcostremapRules);
            vm.viewModelHelper.modelIsValid = vm.lowCostRemap.isValid;
            vm.viewModelHelper.modelErrors = vm.lowCostRemap.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/lowcostremap/updatelowcostremap', vm.lowCostRemap,
               function (result) {

                   $state.go('mpr-lowcostremap-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.lowCostRemap.errors;

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
                vm.viewModelHelper.apiPost('api/lowcostremap/deletelowcostremap', vm.lowCostRemap.LowCostRemapId,//vm.lowCostRemap.lowcostremapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-lowcostremap-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-lowcostremap-list');
        };

        setupRules();
        initialize();
    }
}());
