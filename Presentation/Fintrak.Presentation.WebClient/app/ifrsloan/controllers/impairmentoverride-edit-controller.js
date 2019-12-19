/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ImpairmentOverrideEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ImpairmentOverrideEditController]);

    function ImpairmentOverrideEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'impairmentoverride-edit-view';
        vm.viewName = 'Impairment Override';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.impairmentOverrides = {};
        //vm.parents = [];
        //vm.CCDefintions = [];

        vm.classifications = [
            { Id: 1, Name: 'Specific' },
            { Id: 2, Name: 'Collective' }
        ];
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var impairmentoverrideRules = [];

        var setupRules = function () {

            impairmentoverrideRules.push(new validator.PropertyRule("RefNo", {
                required: { message: "RefNo is required" }
            }));

            impairmentoverrideRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "AccountNo is required" }
            }));

            impairmentoverrideRules.push(new validator.PropertyRule("Classification", {
                required: { message: "Classification is required" }
            }));

             impairmentoverrideRules.push(new validator.PropertyRule("Reason", {
                required: { message: "Reason is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               //intializeLookUp();

                if ($stateParams.impairmentoverrideId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/impairmentoverride/getimpairmentoverride/' + $stateParams.impairmentoverrideId, null,
                   function (result) {
                       vm.impairmentOverrides = result.data;
                       //getCCDefintion();
                       //initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.impairmentOverrides = { RefNo: '', AccountNo: '', Classification: '', Reason: '', Year: '', Active: true };
            }
        }


       

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.impairmentOverrides, impairmentoverrideRules);
            vm.viewModelHelper.modelIsValid = vm.impairmentOverrides.isValid;
            vm.viewModelHelper.modelErrors = vm.impairmentOverrides.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/impairmentoverride/updateimpairmentoverride', vm.impairmentOverrides,
               function (result) {
                   
                   $state.go('ifrsloan-impairmentoverride-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.impairmentOverrides.errors;

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
                vm.viewModelHelper.apiPost('api/impairmentoverride/deleteimpairmentoverride', vm.impairmentOverrides.ImpairmentOverrideId,//vm.impairmentOverride.impairmentoverrideId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-impairmentoverride-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-impairmentoverride-list');
        };

      

        setupRules();
        initialize();
    }
}());
