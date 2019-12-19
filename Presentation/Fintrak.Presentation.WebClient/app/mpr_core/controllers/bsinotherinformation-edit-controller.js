/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BSINOtherInformationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BSINOtherInformationEditController]);

    function BSINOtherInformationEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'bsinotherinformation-edit-view';
        vm.viewName = 'BS Other Information';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.bsInOtherinformation = {};

        vm.currencies = [
         { Name: 'LCY' },
         { Name: 'FCY' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var bsinotherinformationRules = [];

        var setupRules = function () {

            bsinotherinformationRules.push(new validator.PropertyRule("Segment", {
                required: { message: "Segment is required" }
            }));

            bsinotherinformationRules.push(new validator.PropertyRule("OtherCaption", {
                required: { message: "Other Caption is required" }
            }));

            bsinotherinformationRules.push(new validator.PropertyRule("MainCaption", {
                required: { message: "Main Caption is required" }
            }));

            bsinotherinformationRules.push(new validator.PropertyRule("SubCaption", {
                required: { message: "Sub Caption is required" }
            }));

        }



        //var Currency = function () {
        //    vm.viewModelHelper.apiGet('api/currency/availablecurrencies', null,
        //         function (result) {
        //             vm.currencies = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}

        var budgetBSCaptions = function () {
            vm.viewModelHelper.apiGet('api/bsinotherinformation/availablebgetallbsplcaptions', null,
                 function (result) {
                     vm.bsplcaptions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);

        }


        var initialize = function () {
            if (vm.init === false) {

                //Currency();
                budgetBSCaptions();

                if ($stateParams.bsinotherinformationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/bsinotherinformation/getbsinotherinformation/' + $stateParams.bsinotherinformationId, null,
                   function (result) {

                       vm.bsInOtherinformation = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.bsInOtherinformation = { Segment: '', OtherCaption: '', MainCaption: '', SubCaption: '', Currency: '', Active: true, BSIN: true };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.bsInOtherinformation, bsinotherinformationRules);
            vm.viewModelHelper.modelIsValid = vm.bsInOtherinformation.isValid;
            vm.viewModelHelper.modelErrors = vm.bsInOtherinformation.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/bsinotherinformation/updatebsinotherinformation', vm.bsInOtherinformation,
               function (result) {

                   $state.go('mpr-bsinotherinformation-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.bsInOtherinformation.errors;

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
                vm.viewModelHelper.apiPost('api/bsinotherinformation/deletebsinotherinformation', vm.bsInOtherinformation.BSINOtherInformationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-bsinotherinformation-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-bsinotherinformation-list');
        }

        setupRules();
        initialize();

    }
}());
