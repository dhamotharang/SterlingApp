/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BSINOtherInformationTotalLineEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BSINOtherInformationTotalLineEditController]);

    function BSINOtherInformationTotalLineEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'bsinotherinformationtotalline-edit-view';
        vm.viewName = 'BS Other Information Total Line';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.bsInOtherinformationtotalline = {};

        vm.currencies = [
       { Name: 'LCY' },
       { Name: 'FCY' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var bsinotherinformationtotallineRules = [];

        var setupRules = function () {

            bsinotherinformationtotallineRules.push(new validator.PropertyRule("Segment", {
                required: { message: "Segment is required" }
            }));

            bsinotherinformationtotallineRules.push(new validator.PropertyRule("OtherCaption", {
                required: { message: "Other Caption is required" }
            }));

            bsinotherinformationtotallineRules.push(new validator.PropertyRule("MainCaption", {
                required: { message: "Main Caption is required" }
            }));

            bsinotherinformationtotallineRules.push(new validator.PropertyRule("SubCaption", {
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
            vm.viewModelHelper.apiGet('api/bsinotherinformationtotalline/availablebgetallbsplothercaptions', null,
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

                if ($stateParams.bsinotherinformationtotallineId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/bsinotherinformationtotalline/getbsinotherinformationtotalline/' + $stateParams.bsinotherinformationtotallineId, null,
                   function (result) {

                       vm.bsInOtherinformationtotalline = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.bsInOtherinformationtotalline = { Segment: '', OtherCaption: '', MainCaption: '', SubCaption: '', Currency: '', Active: true, BSIN: true };
            }
        }


        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.bsInOtherinformationtotalline, bsinotherinformationtotallineRules);
            vm.viewModelHelper.modelIsValid = vm.bsInOtherinformationtotalline.isValid;
            vm.viewModelHelper.modelErrors = vm.bsInOtherinformationtotalline.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/bsinotherinformationtotalline/updatebsinotherinformationtotalline', vm.bsInOtherinformationtotalline,
               function (result) {

                   $state.go('mpr-bsinotherinformationtotalline-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.bsInOtherinformationTotalLine.errors;

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
                vm.viewModelHelper.apiPost('api/bsinotherinformationtotalline/deletebsinotherinformationtotalline', vm.bsInOtherinformationtotalline.BSINOtherInformationTotalLineId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-bsinotherinformationtotalline-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-bsinotherinformationtotalline-list');
        }

        setupRules();
        initialize();

    }
}());
