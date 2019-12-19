/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IncomeNEAGLSBUEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IncomeNEAGLSBUEditController]);

    function IncomeNEAGLSBUEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'incomeneaglsbu-edit-view';
        vm.viewName = 'Income NEA GL SBU';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.incomeNEAGLSBU = {};

        //vm.ServiceClass = [
        //    { Id: 1, Name: 'Home' },
        //    { Id: 2, Name: 'Away' }
        //];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var incomeneaglsbuRules = [];

        var setupRules = function () {

            incomeneaglsbuRules.push(new validator.PropertyRule("Account", {
                required: { message: "Account is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.incomeneaglsbuId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/incomeneaglsbu/getincomeneaglsbu/' + $stateParams.incomeneaglsbuId, null,
                   function (result) {
                       vm.incomeNEAGLSBU = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.incomeNEAGLSBU = { GLCode: '', SBU: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.incomeNEAGLSBU, incomeneaglsbuRules);
            vm.viewModelHelper.modelIsValid = vm.incomeNEAGLSBU.isValid;
            vm.viewModelHelper.modelErrors = vm.incomeNEAGLSBU.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/incomeneaglsbu/updateincomeneaglsbu', vm.incomeNEAGLSBU,
               function (result) {

                   $state.go('mpr-incomeneaglsbu-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.incomeNEAGLSBU.errors;

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
                vm.viewModelHelper.apiPost('api/incomeneaglsbu/deleteincomeneaglsbu', vm.incomeNEAGLSBU.IncomeNEAGLSBUId,//vm.incomeNEAGLSBU.incomeneaglsbuId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-incomeneaglsbu-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-incomeneaglsbu-list');
        };

        setupRules();
        initialize();
    }
}());
