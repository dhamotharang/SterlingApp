/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CaptionMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CaptionMappingEditController]);

    function CaptionMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'captionmapping-edit-view';
        vm.viewName = 'MPR Caption Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.captionMapping = {};

        vm.value1 = 'Checked'
        vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var captionmappingRules = [];

        var setupRules = function () {

            captionmappingRules.push(new validator.PropertyRule("MPRCaptionName", {
                required: { message: "MPR Caption Name is required" }
            }));

            captionmappingRules.push(new validator.PropertyRule("BudgetCaptionName", {
                required: { message: "Budget Caption Name is required" }
            }));

        }


        ////////////////////////////////Begin////////////////////////////////////////////////

        var mprBSCaptions = function () {
            vm.viewModelHelper.apiGet('api/bsCaption/availablemprbsCaptions', null,
                 function (result) {
                     vm.mprCaptions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var budgetBSCaptions = function () {
            vm.viewModelHelper.apiGet('api/bsCaption/availablebudgetbsCaptions', null,
                 function (result) {
                     vm.budgetCaptions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }


        var mprPLCaptions = function () {
            vm.viewModelHelper.apiGet('api/plCaption/availablemprplCaptions', null,
                 function (result) {
                     vm.mprCaptions = result.data;
                 },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        var budgetPLCaptions = function () {
            vm.viewModelHelper.apiGet('api/plCaption/availablebudgetplCaptions', null,
                 function (result) {
                     vm.budgetCaptions = result.data;
                 },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        ////////////////////////////////End////////////////////////////////////////////////



        var findValue = function (enteredValue) {
            angular.forEach($scope.myData.SerialNumbers, function (object, key) {
                if (key === enteredValue) {
                    $scope.results.push({ serial: key, owner: value[0].Owner });
                }
            });
        };

        vm.onMPRCaptionNameChanged = function () {


            //var found = vm.mprCaptions.filter({ MPRCaptionName: vm.captionMapping.MPRCaptionName });

            //var __found = found;

            angular.forEach(vm.mprCaptions, function (value, key) {



                if (value.CaptionName == vm.captionMapping.MPRCaptionName) {

                    vm.captionMapping.MPRCaptionCode = value.CaptionCode;

                }

            });

        }

        vm.onBudgetCaptionChanged = function () {

            angular.forEach(vm.budgetCaptions, function (value, key) {

                if (value.CaptionName == vm.captionMapping.BudgetCaptionName) {

                    vm.captionMapping.BudgetCaptionCode = value.CaptionCode;

                }


            });

        }

        //////////////////////////////////////////////////
        vm.clickOffBS = function () {
            mprPLCaptions();
            budgetPLCaptions();
            vm.captionMapping.MPRCaptionCode = '';
            vm.captionMapping.BudgetCaptionCode = '';
            vm.captionMapping.CaptionIndicator = 'PL'
            vm.bs = false;
        };

        vm.clickOffPL = function () {
            mprBSCaptions();
            budgetBSCaptions();
            vm.captionMapping.MPRCaptionCode = '';
            vm.captionMapping.BudgetCaptionCode = '';
            vm.captionMapping.CaptionIndicator = 'BS'
            vm.pl = false;
        };

        var clickOffBS = function () {
            mprPLCaptions();
            budgetPLCaptions();
            vm.captionMapping.MPRCaptionCode = '';
            vm.captionMapping.BudgetCaptionCode = '';
            vm.captionMapping.CaptionIndicator = 'PL'
            vm.bs = false;
            vm.pl = true;
        };

        var clickOffPL = function () {
            mprBSCaptions();
            budgetBSCaptions();
            vm.captionMapping.MPRCaptionCode = '';
            vm.captionMapping.BudgetCaptionCode = '';
            vm.captionMapping.CaptionIndicator = 'BS'
            vm.pl = false;
            vm.bs = true;
        };

        //vm.clickOffBS();
        //vm.clickOffPL();
        /////////////////////////////////////////////////


        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.captionmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/captionmapping/getcaptionmapping/' + $stateParams.captionmappingId, null,
                   function (result) {

                       if (result.data.CaptionIndicator == 'BS') { clickOffPL(); } else { clickOffBS(); }

                       vm.captionMapping = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.captionMapping = { MPRCaptionCode: '', MPRCaptionName: '', BudgetCaptionCode: '', BudgetCaptionName: '', CaptionIndicator: '', Active: true };
            }
        }

        //var intializeLookUp = function () {
        //    getTeams(1);
        //}

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.captionMapping, captionmappingRules);
            vm.viewModelHelper.modelIsValid = vm.captionMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.captionMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/captionmapping/updatecaptionMapping', vm.captionMapping,
               function (result) {

                   $state.go('mpr-captionmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.captionMapping.errors;

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
                vm.viewModelHelper.apiPost('api/captionmapping/deletecaptionmapping', vm.captionMapping.CaptionMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-captionmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-captionmapping-list');
        }

        setupRules();
        initialize();

    }
}());
