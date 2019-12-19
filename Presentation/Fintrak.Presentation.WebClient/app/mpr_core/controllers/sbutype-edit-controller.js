/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SbuTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SbuTypeEditController]);

    function SbuTypeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'sbutype-edit-view';
        vm.viewName = 'SBU Type';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sbuType = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var sbutypeRules = [];

        var setupRules = function () {

            sbutypeRules.push(new validator.PropertyRule("Sbu", {
                required: { message: "Sbu is required" }
            }));

            sbutypeRules.push(new validator.PropertyRule("Type", {
                required: { message: "Type is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.sbutypeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/sbutype/getsbutype/' + $stateParams.sbutypeId, null,
                   function (result) {
                       vm.sbuType = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.sbuType = { Sbu: '', Type: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.sbuType, sbutypeRules);
            vm.viewModelHelper.modelIsValid = vm.sbuType.isValid;
            vm.viewModelHelper.modelErrors = vm.sbuType.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/sbutype/updatesbutype', vm.sbuType,
               function (result) {

                   $state.go('mpr-sbutype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.sbuType.errors;

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
                vm.viewModelHelper.apiPost('api/sbutype/deletesbutype', vm.sbuType.SbuTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-sbutype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-sbutype-list');
        };

        setupRules();
        initialize();
    }
}());
