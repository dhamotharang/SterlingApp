/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPIMetricEditController",
                    ['$rootScope', '$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPIMetricEditController]);

    function KPIMetricEditController($rootScope,$scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'metric-edit-view';
        vm.viewName = 'KPI';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.kpi = {};

        vm.directions = [
            { Id: 1, Name: 'Down' },
            { Id: 2, Name: 'Up' },
            { Id: 3, Name: 'None' }
        ];

        vm.periodTypes = [
           { Id: 1, Name: 'Daily' },
           { Id: 2, Name: 'Weekly' },
           { Id: 3, Name: 'Monthly' },
           { Id: 4, Name: 'Quarterly' },
           { Id: 5, Name: 'Yearly' }
        ];

        vm.aggregateMethods = [
            { Id: 1, Name: 'Average' },
            { Id: 2, Name: 'Sum' }
        ];

        vm.scoreAttributes = [
            { Id: '[Actual]', Name: 'Actual' },
            { Id: '[Target]', Name: 'Target' }
        ];

        vm.categories = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var kpiRules = [];

        var setupRules = function () {
          
            kpiRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            kpiRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            kpiRules.push(new validator.PropertyRule("PeriodType", {
                notZero: { message: "Period Type is required" }
            }));

            kpiRules.push(new validator.PropertyRule("Direction", {
                notZero: { message: "Direction is required" }
            }));

            kpiRules.push(new validator.PropertyRule("AggregateMethod", {
                notZero: { message: "Aggregate Method is required" }
            }));

            //kpiRules.push(new validator.PropertyRule("Period", {
            //    notZero: { message: "Period is required" }
            //}));

            //kpiRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));

            //kpiRules.push(new validator.PropertyRule("CategoryCode", {
            //    required: { message: "Category Code is required" }
            //}));

            kpiRules.push(new validator.PropertyRule("ScoreFormula", {
                required: { message: "Score Formula is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.metricId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scdkpi/getkpi/' + $stateParams.metricId, null,
                   function (result) {
                       vm.kpi = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.kpi = { Period: 0, Year: '',Code:'',Name:'',PeriodType: 1, Direction:1,CategoryCode:'',IsKPICalculated: false,AggregateMethod:1,IsTargetCalculated: false,ScoreFormula:'',Description:'', Active: true };
            }
        }


        var intializeLookUp = function () {
            getCategories();
            getKPIs();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.kpi, kpiRules);
            vm.viewModelHelper.modelIsValid = vm.kpi.isValid;
            vm.viewModelHelper.modelErrors = vm.kpi.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/scdkpi/updatekpi', vm.kpi,
               function (result) {
                   
                   $state.go('scd-metric-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.kpi.errors;

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
                vm.viewModelHelper.apiPost('api/scdkpi/deletekpi', vm.kpi.KPIId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-metric-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-metric-list');
        };

        vm.kpiFormularChanged = function (value) {
            $rootScope.$broadcast('updateKPIFormular', value);
        }

        vm.scoreFormularChanged = function (value) {
            $rootScope.$broadcast('updateScoreFormular', value);
        }

        var getKPIs = function () {
            vm.viewModelHelper.apiGet('api/scdkpi/availablekpi', null,
                 function (result) {
                     vm.kpis = result.data;

                 },
                 function (result) {
                     toastr.error('Fail to load metrics.', 'Fintrak');
                 }, null);
        }

        var getCategories = function () {
            vm.viewModelHelper.apiGet('api/category/availablecategory', null,
                 function (result) {
                     vm.categories = result.data;
                   
                 },
                 function (result) {
                     toastr.error('Fail to load kpi categories.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
