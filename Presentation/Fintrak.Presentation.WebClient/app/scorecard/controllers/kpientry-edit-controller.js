/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPIEntryEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPIEntryEditController]);

    function KPIEntryEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'kpientry-edit-view';
        vm.viewName = 'KPI Data Entry';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.kpientry = {};

        vm.kpis = [];
        vm.staffs = [];
        vm.classifications = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var kpientryRules = [];

        var setupRules = function () {
          
            kpientryRules.push(new validator.PropertyRule("Period", {
                notZero: { message: "Period is required" }
            }));

            kpientryRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            kpientryRules.push(new validator.PropertyRule("KPICode", {
                required: { message: "KPI Code is required" }
            }));

            kpientryRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "Mis Code is required" }
            }));

            kpientryRules.push(new validator.PropertyRule("StaffCode", {
                required: { message: "Staff Code is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.kpientryId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scdkpientry/getkpientry/' + $stateParams.kpientryId, null,
                   function (result) {
                       vm.kpientry = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.kpientry = { Period: 0, Year: '',KPICode:'',MisCode:'',StaffCode:'',Target:0,Actual:0,Score:0,Date : new Date(), Active: true };
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
            validator.ValidateModel(vm.kpientry, kpientryRules);
            vm.viewModelHelper.modelIsValid = vm.kpientry.isValid;
            vm.viewModelHelper.modelErrors = vm.kpientry.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/scdkpientry/updatekpientry', vm.kpientry,
               function (result) {
                   
                   $state.go('scd-kpientry-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.kpientry.errors;

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
                vm.viewModelHelper.apiPost('api/scdkpientry/deletekpientry', vm.kpientry.EntryId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-kpientry-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-kpientry-list');
        };

        var getKPIs = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamDefinitions', null,
                 function (result) {
                     vm.kpis = result.data;
                   
                 },
                 function (result) {
                     toastr.error('Fail to load metrics.', 'Fintrak');
                 }, null);
        }

        var getStaffs = function () {
            vm.viewModelHelper.apiGet('api/staff/availablestaff', null,
                 function (result) {
                     vm.staffs = result.data;
                  
                 },
                 function (result) {
                     toastr.error('Fail to load staffs.', 'Fintrak');
                 }, null);
        }

        var getClassifications = function () {
            vm.viewModelHelper.apiGet('api/teamclassification/availableteamclassification', null,
                 function (result) {
                     vm.classifications = result.data;

                 },
                 function (result) {
                     toastr.error('Fail to load classifications.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
