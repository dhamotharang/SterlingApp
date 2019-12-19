/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CalendarEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CalendarEditController]);

    function CalendarEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Finstat';
        vm.view = 'calendar-edit-view';
        vm.viewName = 'Calendar';
        vm.status = false;
        vm.openedRunDate = false;
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.calendars = {};
        vm.calendarsobj = {};
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var calendarRules = [];

        var setupRules = function () {

            calendarRules.push(new validator.PropertyRule("ThisDate", {
                required: { message: "Date is required" }
            }));

            calendarRules.push(new validator.PropertyRule("FullDescription", {
                required: { message: "FullDescription is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.CalId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/calendar/getcalendar/' + $stateParams.CalId, null,
                   function (result) {
                       vm.calendars = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.calendars = {
                        //ThisDate: new date(),
                        FullDEscription: '',                       
                        Active: true
                    };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.calendars, calendarRules);
            vm.viewModelHelper.modelIsValid = vm.calendars.isValid;
            vm.viewModelHelper.modelErrors = vm.calendars.errors;
            if (vm.viewModelHelper.modelIsValid) {        
                vm.viewModelHelper.apiPost('api/calendar/updatecalendar', vm.calendars,
               function (result) {                   
                   $state.go('finstat-calendar-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.calendars.errors;

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
                vm.viewModelHelper.apiPost('api/calendar/deletecalendar', vm.calendars.CalId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('finstat-calendar-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('finstat-calendar-list');
        };

        vm.openRunDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedRunDate = true;
        }
        setupRules();
        initialize();

    }
}());
