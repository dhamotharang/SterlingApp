/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPIDataEntryEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPIDataEntryEditController]);

    function KPIDataEntryEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'dataentryedit-view';
        vm.viewName = 'KPI Data Entry';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.dataentry = {};

        vm.kpis = [];
        vm.staffs = [];
        vm.teams = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var dataentryRules = [];

        var setupRules = function () {
          
            //dataentryRules.push(new validator.PropertyRule("Period", {
            //    notZero: { message: "Period is required" }
            //}));

            dataentryRules.push(new validator.PropertyRule("StaffCode", {
                notZero: { message: "Staff is required" }
            }));

            dataentryRules.push(new validator.PropertyRule("MISCode", {
                notZero: { message: "MisCode is required" }
            }));

            //dataentryRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));

            dataentryRules.push(new validator.PropertyRule("KPICode", {
                required: { message: "KPI Code is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.dataentryId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/kpientry/getkpiEntry/' + $stateParams.dataentryId, null,
                   function (result) {
                       vm.dataentry = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.dataentry = { Period: 0, Year: '',KPICode:'',StaffCode:'',MISCode: '', Active: true };
            }
        }


        var intializeLookUp = function () {
            getKPIs();
            getTeams();
            getStaffs();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.dataentry, dataentryRules);
            vm.viewModelHelper.modelIsValid = vm.dataentry.isValid;
            vm.viewModelHelper.modelErrors = vm.dataentry.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/kpientry/updatekpiEntry', vm.dataentry,
               function (result) {
                   
                   $state.go('scd-dataentry-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.dataentry.errors;

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
                vm.viewModelHelper.apiPost('api/kpientry/deletekpiEntry', vm.dataentry.DataEntryId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-dataentry-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-dataentry-list');
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

        var getTeams = function () {
            vm.viewModelHelper.apiGet('api/teammap/availableteamMap', null,
                 function (result) {
                     vm.teams = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load team classification.', 'Fintrak');
                 }, null);
        }

        vm.openDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate = true;
        }

        setupRules();
        initialize(); 
    }
}());
