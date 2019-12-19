/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPIThresholdEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPIThresholdEditController]);

    function KPIThresholdEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'threshold-edit-view';
        vm.viewName = 'KPI Threshold';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.threshold = {};

        vm.kpis = [];
        vm.staffs = [];
        vm.classifications = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var thresholdRules = [];

        var setupRules = function () {
          
            thresholdRules.push(new validator.PropertyRule("Period", {
                notZero: { message: "Period is required" }
            }));

            thresholdRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            thresholdRules.push(new validator.PropertyRule("KPICode", {
                required: { message: "KPI Code is required" }
            }));

            thresholdRules.push(new validator.PropertyRule("TeamClassificationCode", {
                required: { message: "Classification Code is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.thresholdId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/threshold/getthreshold/' + $stateParams.thresholdId, null,
                   function (result) {
                       vm.threshold = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.threshold = { Period: 0, Year: '',KPICode:'',TeamClassificationCode:'', Active: true };
            }
        }


        var intializeLookUp = function () {
            getKPIs();
            getClassifications();
            getStaffs();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.threshold, thresholdRules);
            vm.viewModelHelper.modelIsValid = vm.threshold.isValid;
            vm.viewModelHelper.modelErrors = vm.threshold.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/threshold/updatethreshold', vm.threshold,
               function (result) {
                   
                   $state.go('scd-threshold-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.threshold.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/threshold/deletethreshold', vm.threshold.ThresholdId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-threshold-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-threshold-list');
        };

        var getKPIs = function () {
            vm.viewModelHelper.apiGet('api/scdkpi/availablekpi', null,
                 function (result) {
                     vm.kpis = result.data;

                 },
                 function (result) {
                     toastr.error('Fail to load metrics.', 'Fintrak');
                 }, null);
        }

        var getStaffs = function () {
            vm.viewModelHelper.apiGet('api/staff/availablestaffs', null,
                 function (result) {
                     vm.staffs = result.data;

                 },
                 function (result) {
                     toastr.error('Fail to load staffs.', 'Fintrak');
                 }, null);
        }

        var getClassifications = function () {
            vm.viewModelHelper.apiGet('api/scdteamclassification/availableteamclassification', null,
                 function (result) {
                     vm.classifications = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load team classification.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
