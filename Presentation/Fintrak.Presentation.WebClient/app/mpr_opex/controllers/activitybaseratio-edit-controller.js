/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ActivityBaseRatioEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ActivityBaseRatioEditController]);

    function ActivityBaseRatioEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'activitybaseratio-edit-view';
        vm.viewName = 'Activity Base Ratio';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.activityBaseRatio = {};
       
        vm.ServiceClass = [
            { Id: 1, Name: 'Home' },
            { Id: 2, Name: 'Away' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var activitybaseratioRules = [];

        var setupRules = function () {

            activitybaseratioRules.push(new validator.PropertyRule("ServiceClass", {
                required: { message: "Service Class is required" }
            }));

            activitybaseratioRules.push(new validator.PropertyRule("Ratio", {
                required: { message: "Ratio is required" }
            }));
                     
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               // intializeLookUp();

                if ($stateParams.activitybaseratioId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/activitybaseratio/getactivitybaseratio/' + $stateParams.activitybaseratioId, null,
                   function (result) {
                       vm.activityBaseRatio = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.activityBaseRatio = { ServiceClass: '',Ratio: '' , Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.activityBaseRatio, activitybaseratioRules);
            vm.viewModelHelper.modelIsValid = vm.activityBaseRatio.isValid;
            vm.viewModelHelper.modelErrors = vm.activityBaseRatio.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/activitybaseratio/updateactivitybaseratio', vm.activityBaseRatio,
               function (result) {
                   
                   $state.go('mpr-activitybaseratio-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.activityBaseRatio.errors;

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
                vm.viewModelHelper.apiPost('api/activitybaseratio/deleteactivitybaseratio', vm.activityBaseRatio.ActivityBaseRatioId,//vm.activityBaseRatio.activitybaseratioId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-activitybaseratio-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-activitybaseratio-list');
        };

        setupRules();
        initialize();
    }
}());
