/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPIActualMappingEditController",
                    ['$rootScope','$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPIActualMappingEditController]);

    function KPIActualMappingEditController($rootScope,$scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'kpiactualmapping-edit-view';
        vm.viewName = 'KPI Actual Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.actualMapping = {};

        vm.kpis = [];
        vm.actualCaptions = [];
        vm.actualMappings = [];
        vm.selectedActualCaption=''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var actualMappingRules = [];

        var setupRules = function () {
          
            actualMappingRules.push(new validator.PropertyRule("Period", {
                notZero: { message: "Period is required" }
            }));

            actualMappingRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            actualMappingRules.push(new validator.PropertyRule("KPICode", {
                required: { message: "KPI Code is required" }
            }));

            actualMappingRules.push(new validator.PropertyRule("Formula", {
                required: { message: "Formula is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups actualMappingId
                intializeLookUp();
                if ($stateParams.actualmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/kpiActualMap/getkpiActualMap/' + $stateParams.actualmappingId, null,
                   function (result) {
                       vm.actualMapping = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.actualMapping = { Period: 0, Year: '',KPICode:'',Formula:'', Active: true };
            }
        }


        var intializeLookUp = function () {
            getKPIs();
            getActualCaptions();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.actualMapping, actualMappingRules);
            vm.viewModelHelper.modelIsValid = vm.actualMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.actualMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/kpiActualMap/updatekpiActualMap', vm.actualMapping,
               function (result) {
                   
                   $state.go('scd-actualmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.actualMapping.errors;

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
                vm.viewModelHelper.apiPost('api/kpiActualMap/deletekpiActualMap', vm.actualMapping.MapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-actualmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-actualmapping-list');
        };

        vm.actualFormularChanged = function (value) {
            $rootScope.$broadcast('updateKPIFormular', value);
        }

        var getKPIs = function () {
            vm.viewModelHelper.apiGet('api/scdkpi/availablekpi', null,
                 function (result) {
                     vm.kpis = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load metrics.', 'Fintrak');
                 }, null);
        }


        var getActualCaptions = function () {
            vm.viewModelHelper.apiGet('api/actual/getcaptions', null,
                 function (result) {
                     vm.actualCaptions = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
