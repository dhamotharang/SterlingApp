/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RatiosEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RatiosEditController]);

    function RatiosEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'ratios-edit-view';
        vm.viewName = 'MPR Ratios';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ratios = {};

        //vm.value1 = 'Checked'
        //vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ratiosRules = [];

        var setupRules = function () {

            ratiosRules.push(new validator.PropertyRule("MPRCaption", {
                required: { message: "MPR Caption is required" }
            }));

            ratiosRules.push(new validator.PropertyRule("Numerator", {
                required: { message: "Numerator is required" }
            }));

            ratiosRules.push(new validator.PropertyRule("Denominator", {
                required: { message: "Denominator is required" }
            }));

        }

        var mprNumerator = function () {
            vm.viewModelHelper.apiGet('api/ratiocaptionmapping/availableratioCaptionMappings', null,
                 function (result) {
                     vm.mprNumerators = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var mprDenominator = function () {
            vm.viewModelHelper.apiGet('api/ratiocaptionmapping/availableratioCaptionMappings', null,
                 function (result) {
                     vm.mprDenominators = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }



        var initialize = function () {
            if (vm.init === false) {

                if ($stateParams.ratiosId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ratios/getratios/' + $stateParams.ratiosId, null,
                   function (result) {

                       vm.ratios = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ratios = { MainCaption: '', Numerator: '', Denominator: '', ProRatio: '', Bsin: '', Active: true };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ratios, ratiosRules);
            vm.viewModelHelper.modelIsValid = vm.ratios.isValid;
            vm.viewModelHelper.modelErrors = vm.ratios.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/ratios/updateratios', vm.ratios,
               function (result) {

                   $state.go('mpr-ratios-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.ratios.errors;

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
                vm.viewModelHelper.apiPost('api/ratios/deleteratios', vm.ratios.RatiosId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-ratios-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-ratios-list');
        }


        mprNumerator();
        mprDenominator();
        setupRules();
        initialize();


    }
}());
