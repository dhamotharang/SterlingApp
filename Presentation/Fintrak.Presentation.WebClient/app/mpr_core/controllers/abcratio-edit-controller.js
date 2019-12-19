/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AbcRatioEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        AbcRatioEditController]);

    function AbcRatioEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'abcratio-edit-view';
        vm.viewName = 'ABC Ratio';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.abcRatio = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var abcratioRules = [];

        var setupRules = function () {

            abcratioRules.push(new validator.PropertyRule("Branch", {
                required: { message: "Branch is required" }
            }));

            abcratioRules.push(new validator.PropertyRule("Percentage", {
                required: { message: "Percentage is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.abcratioId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/abcratio/getabcratio/' + $stateParams.abcratioId, null,
                   function (result) {
                       vm.abcRatio = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.abcRatio = { Branch: '', Percentage: 0, Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.abcRatio, abcratioRules);
            vm.viewModelHelper.modelIsValid = vm.abcRatio.isValid;
            vm.viewModelHelper.modelErrors = vm.abcRatio.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/abcratio/updateabcratio', vm.abcRatio,
               function (result) {

                   $state.go('mpr-abcratio-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.abcRatio.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/abcratio/deleteabcratio', vm.abcRatio.AbcRatioId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-abcratio-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-abcratio-list');
        };

        setupRules();
        initialize();
    }
}());
