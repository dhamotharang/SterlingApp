/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ScheduleTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ScheduleTypeEditController]);

    function ScheduleTypeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS_LOANS';
        vm.view = 'scheduletype-edit-view';
        vm.viewName = 'Schedule Types';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.scheduleTypes = {};   
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var scheduletypeRules = [];

        var setupRules = function () {

            scheduletypeRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            scheduletypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

               scheduletypeRules.push(new validator.PropertyRule("ActionName", {
                required: { message: "ActionName is required" }
            }));     
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               //intializeLookUp();

                if ($stateParams.scheduletypeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scheduletype/getscheduletype/' + $stateParams.scheduletypeId, null,
                   function (result) {
                       vm.scheduleTypes = result.data;
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.scheduleTypes = { Code: '', Name: '', ActionName: '', IsDefault: false, CompanyCode: '', Active: true };
            }
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.scheduleTypes, scheduletypeRules);
            vm.viewModelHelper.modelIsValid = vm.scheduleTypes.isValid;
            vm.viewModelHelper.modelErrors = vm.scheduleTypes.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/scheduletype/updatescheduletype', vm.scheduleTypes,
               function (result) {
                   
                   $state.go('ifrsloan-scheduletype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.scheduleTypes.errors;

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
                vm.viewModelHelper.apiPost('api/scheduletype/deletescheduletype', vm.scheduleTypes.ScheduleTypeId,//vm.scheduleType.scheduletypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrsloan-scheduletype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('ifrsloan-scheduletype-list');
        };
        setupRules();
        initialize();
    }
}());
