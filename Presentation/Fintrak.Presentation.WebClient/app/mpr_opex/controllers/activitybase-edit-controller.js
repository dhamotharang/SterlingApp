/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ActivityBaseEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ActivityBaseEditController]);

    function ActivityBaseEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'activitybase-edit-view';
        vm.viewName = 'Activity Base';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.activityBase = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var activitybaseRules = [];

        var setupRules = function () {

            activitybaseRules.push(new validator.PropertyRule("ServiceCode", {
                required: { message: "Service Code is required" }
            }));
            activitybaseRules.push(new validator.PropertyRule("ServiceCategory", {
                required: { message: "ServiceCategory is required" }
            }));
            activitybaseRules.push(new validator.PropertyRule("ServiceDescription", {
                required: { message: "Service Description is required" }
            }));
            activitybaseRules.push(new validator.PropertyRule("Weight", {
                required: { message: "Weight is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                // intializeLookUp();

                if ($stateParams.activitybaseId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/activitybase/getactivitybase/' + $stateParams.activitybaseId, null,
                   function (result) {
                       vm.activityBase = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.activityBase = { ServiceCode: '', ServiceCategory: '', ServiceDescription: '', Weight: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.activityBase, activitybaseRules);
            vm.viewModelHelper.modelIsValid = vm.activityBase.isValid;
            vm.viewModelHelper.modelErrors = vm.activityBase.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/activitybase/updateactivitybase', vm.activityBase,
               function (result) {

                   $state.go('mpr-activitybase-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.activityBase.errors;

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
                vm.viewModelHelper.apiPost('api/activitybase/deleteactivitybase', vm.activityBase.ActivityBaseId,//vm.activityBase.activitybaseId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-activitybase-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-activitybase-list');
        };

        setupRules();
        initialize();
    }
}());
