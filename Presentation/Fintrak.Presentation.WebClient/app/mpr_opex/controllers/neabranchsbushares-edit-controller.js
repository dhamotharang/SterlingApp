/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NEABranchSBUSharesEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NEABranchSBUSharesEditController]);

    function NEABranchSBUSharesEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'neabranchsbushares-edit-view';
        vm.viewName = 'NEA Branch SBU Shares';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.nEABranchSBUShares = {};

        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var neabranchsbusharesRules = [];

        var setupRules = function () {

            neabranchsbusharesRules.push(new validator.PropertyRule("Branch", {
                required: { message: "Branch is required" }
            }));
            neabranchsbusharesRules.push(new validator.PropertyRule("SBU", {
                required: { message: "SBU is required" }
            }));
            neabranchsbusharesRules.push(new validator.PropertyRule("Year", {
                required: { message: "Ratio is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.neabranchsbusharesId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/neabranchsbushares/getneabranchsbushares/' + $stateParams.neabranchsbusharesId, null,
                   function (result) {
                       vm.nEABranchSBUShares = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.nEABranchSBUShares = { Branch: '', SBU: '', Ratio: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.nEABranchSBUShares, neabranchsbusharesRules);
            vm.viewModelHelper.modelIsValid = vm.nEABranchSBUShares.isValid;
            vm.viewModelHelper.modelErrors = vm.nEABranchSBUShares.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/neabranchsbushares/updateneabranchsbushares', vm.nEABranchSBUShares,
               function (result) {

                   $state.go('mpr-neabranchsbushares-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.nEABranchSBUShares.errors;

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
                vm.viewModelHelper.apiPost('api/neabranchsbushares/deleteneabranchsbushares', vm.nEABranchSBUShares.NEABranchSBUSharesId,//vm.nEABranchSBUShares.neabranchsbusharesId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-neabranchsbushares-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-neabranchsbushares-list');
        };

        setupRules();
        initialize();
    }
}());
