/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("RatioCaptionMappingEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        RatioCaptionMappingEditController]);

    function RatioCaptionMappingEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'ratiocaptionmapping-edit-view';
        vm.viewName = 'MPR Ratio Caption Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ratioCaptionMapping = {};

        vm.value1 = 'Checked'
        vm.value2 = ''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var ratiocaptionmappingRules = [];

        var setupRules = function () {

            ratiocaptionmappingRules.push(new validator.PropertyRule("RatioCaption", {
                required: { message: "Ratio Caption is required" }
            }));

            ratiocaptionmappingRules.push(new validator.PropertyRule("ReportCaption", {
                required: { message: "Report Captione is required" }
            }));

        }


        ////////////////////////////////Begin////////////////////////////////////////////////

        var mprBSCaptions = function () {
            vm.viewModelHelper.apiGet('api/bsCaption/availablemprbscaption', null,
                 function (result) {
                     vm.mprCaptions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        //var budgetBSCaptions = function () {
        //    vm.viewModelHelper.apiGet('api/bsCaption/availablebudgetbsCaptions', null,
        //         function (result) {
        //             vm.budgetCaptions = result.data;
        //         },
        //         function (result) {
        //             toastr.error(result.data, 'Fintrak');
        //         }, null);
        //}


        var mprPLCaptions = function () {
            vm.viewModelHelper.apiGet('api/plCaption/availablemprplCaption', null,
                 function (result) {
                     vm.mprCaptions = result.data;
                 },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        //var budgetPLCaptions = function () {
        //    vm.viewModelHelper.apiGet('api/plCaption/availablebudgetplCaptions', null,
        //         function (result) {
        //             vm.budgetCaptions = result.data;
        //         },
        //        function (result) {
        //            toastr.error(result.data, 'Fintrak');
        //        }, null);
        //}

        ////////////////////////////////End////////////////////////////////////////////////



        var findValue = function (enteredValue) {
            angular.forEach($scope.myData.SerialNumbers, function (object, key) {
                if (key === enteredValue) {
                    $scope.results.push({ serial: key, owner: value[0].Owner });
                }
            });
        };

        //vm.onMPRCaptionNameChanged = function () {


        //    //var found = vm.mprCaptions.filter({ MPRCaptionName: vm.ratioCaptionMapping.MPRCaptionName });

        //    //var __found = found;

        //    angular.forEach(vm.mprCaptions, function (value, key) {



        //        if (value.CaptionName == vm.ratioCaptionMapping.MPRCaptionName) {

        //            vm.ratioCaptionMapping.MPRCaptionCode = value.CaptionCode;

        //        }

        //    });

        //}

        //vm.onBudgetCaptionChanged = function () {

        //    angular.forEach(vm.budgetCaptions, function (value, key) {

        //        if (value.CaptionName == vm.ratioCaptionMapping.BudgetCaptionName) {

        //            vm.ratioCaptionMapping.BudgetCaptionCode = value.CaptionCode;

        //        }


        //    });

        //}

        //////////////////////////////////////////////////
        vm.clickOffBS = function () {
            mprPLCaptions();
            //budgetPLCaptions();
            vm.ratioCaptionMapping.MPRCaptionCode = '';
            vm.ratioCaptionMapping.BudgetCaptionCode = '';
            vm.ratioCaptionMapping.ReportType = 'PL'
            vm.bs = false;
        };

        vm.clickOffPL = function () {
            mprBSCaptions();
            //budgetBSCaptions();
            vm.ratioCaptionMapping.MPRCaptionCode = '';
            vm.ratioCaptionMapping.BudgetCaptionCode = '';
            vm.ratioCaptionMapping.ReportType = 'BS'
            vm.pl = false;
        };

        var clickOffBS = function () {
            mprPLCaptions();
            //budgetPLCaptions();
            vm.ratioCaptionMapping.MPRCaptionCode = '';
            vm.ratioCaptionMapping.BudgetCaptionCode = '';
            vm.ratioCaptionMapping.ReportType = 'PL'
            vm.bs = false;
            vm.pl = true;
        };

        var clickOffPL = function () {
            mprBSCaptions();
            //budgetBSCaptions();
            vm.ratioCaptionMapping.MPRCaptionCode = '';
            vm.ratioCaptionMapping.BudgetCaptionCode = '';
            vm.ratioCaptionMapping.ReportType = 'BS'
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

                if ($stateParams.ratiocaptionmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ratiocaptionmapping/getratiocaptionmapping/' + $stateParams.ratiocaptionmappingId, null,
                   function (result) {

                       if (result.data.ReportType == 'BS') { clickOffPL(); } else { clickOffBS(); }

                       vm.ratioCaptionMapping = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.ratioCaptionMapping = { MPRCaptionName: '', ReportType: '', Active: true };
                //vm.ratioCaptionMapping = { MPRCaptionCode: '', MPRCaptionName: '', BudgetCaptionCode: '', BudgetCaptionName: '', ReportType: '', Active: true };
            }
        }

        //var intializeLookUp = function () {
        //    getTeams(1);
        //}

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ratioCaptionMapping, ratiocaptionmappingRules);
            vm.viewModelHelper.modelIsValid = vm.ratioCaptionMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.ratioCaptionMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/ratiocaptionmapping/updateratioCaptionMapping', vm.ratioCaptionMapping,
               function (result) {

                   $state.go('mpr-ratiocaptionmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.ratioCaptionMapping.errors;

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
                vm.viewModelHelper.apiPost('api/ratiocaptionmapping/deleteratiocaptionmapping', vm.ratioCaptionMapping.RatioCaptionMappingId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-ratiocaptionmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-ratiocaptionmapping-list');
        }

        setupRules();
        initialize();

    }
}());
