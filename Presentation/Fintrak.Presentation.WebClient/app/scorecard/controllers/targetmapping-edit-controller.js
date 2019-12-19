/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("KPITargetMappingEditController",
                    ['$rootScope','$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        KPITargetMappingEditController]);

    function KPITargetMappingEditController($rootScope,$scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'kpitargetmapping-edit-view';
        vm.viewName = 'KPI Target Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.targetMapping = {};

        vm.kpis = [];
        vm.targetMappings = [];
        vm.targetCaptions = [];
        vm.selectedTargetCaption=''

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var targetMappingRules = [];

        var setupRules = function () {
          
            targetMappingRules.push(new validator.PropertyRule("Period", {
                notZero: { message: "Period is required" }
            }));

            targetMappingRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            targetMappingRules.push(new validator.PropertyRule("KPICode", {
                required: { message: "KPI Code is required" }
            }));

            targetMappingRules.push(new validator.PropertyRule("Formula", {
                required: { message: "Formula is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups targetMappingId
                intializeLookUp();
                if ($stateParams.targetmappingId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/kpiTargetMap/getkpiTargetMap/' + $stateParams.targetmappingId, null,
                   function (result) {
                       vm.targetMapping = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.targetMapping = { Period: 0, Year: '',KPICode:'',Formula:'', Active: true };
            }
        }


        var intializeLookUp = function () {
            getKPIs();
            getCaptions();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.targetMapping, targetMappingRules);
            vm.viewModelHelper.modelIsValid = vm.targetMapping.isValid;
            vm.viewModelHelper.modelErrors = vm.targetMapping.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/kpiTargetMap/updatekpiTargetMap', vm.targetMapping,
               function (result) {
                   
                   $state.go('scd-targetmapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.targetMapping.errors;

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
                vm.viewModelHelper.apiPost('api/kpiTargetMap/deletekpiTargetMap', vm.targetMapping.MapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-targetmapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-targetmapping-list');
        };

         vm.targetFormularChanged = function (value) {
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

        var getCaptions = function () {
            vm.viewModelHelper.apiGet('api/target/getcaptions', null,
                 function (result) {
                     vm.captions = result.data;
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
