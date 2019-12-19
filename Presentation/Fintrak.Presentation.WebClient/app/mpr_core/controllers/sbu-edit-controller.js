/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SbuEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SbuEditController]);

    function SbuEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'sbu-edit-view';
        vm.viewName = 'SBU';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sbu = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var sbuRules = [];

        var setupRules = function () {

            sbuRules.push(new validator.PropertyRule("SbuCode", {
                required: { message: "SbuCode is required" }
            }));

            sbuRules.push(new validator.PropertyRule("Description", {
                required: { message: "Description is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                ////intializeLookUp();

                if ($stateParams.sbuId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/sbu/getsbu/' + $stateParams.sbuId, null,
                   function (result) {
                       vm.sbu = result.data;

                       //initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.sbu = { SbuCode: '', Description: '', Active: true };
            }
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.sbu, sbuRules);
            vm.viewModelHelper.modelIsValid = vm.sbu.isValid;
            vm.viewModelHelper.modelErrors = vm.sbu.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/sbu/updatesbu', vm.sbu,
               function (result) {

                   $state.go('mpr-sbu-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.sbu.errors;

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
                vm.viewModelHelper.apiPost('api/sbu/deletesbu', vm.sbu.SbuId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-sbu-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-sbu-list');
        };

        setupRules();
        initialize();
    }
}());
