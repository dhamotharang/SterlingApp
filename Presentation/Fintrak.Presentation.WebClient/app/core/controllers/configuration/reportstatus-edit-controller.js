/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ReportStatusEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ReportStatusEditController]);

    function ReportStatusEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Configuration';
        vm.view = 'reportstatus-edit-view';
        vm.viewName = 'Report View status';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.reportStatus = {};

        vm.solutions = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
        var reportStatusRules = [];

       // vm.openedRunDate = false;

        var setupRules = function () {
          
            reportStatusRules.push(new validator.PropertyRule("SolutionId", {
                required: { message: "Solution is required" }
            }));

            //reportStatusRules.push(new validator.PropertyRule("Status", {
            //    mustBeDate: { message: "Please enter a valid date" }
            //}));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intialiazeLookUp();

                if ($stateParams.statusId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/reportstatus/getreportstatus/' + $stateParams.statusId, null,
                   function (result) {
                       vm.reportStatus = result.data;

                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.reportStatus = { Status:false , Active: true };
            }
        }

        var intialiazeLookUp = function () {
            getSolutions();
        }

        var initialView = function () {
        
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.reportStatus, reportStatusRules);
            vm.viewModelHelper.modelIsValid = vm.reportStatus.isValid;
            vm.viewModelHelper.modelErrors = vm.reportStatus.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/reportstatus/updatereportstatus', vm.reportStatus,
               function (result) {
                   
                   $state.go('core-reportstatus-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.reportStatus.errors;

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
                vm.viewModelHelper.apiPost('api/reportStatus/deletereportStatus', vm.reportStatus.StatusId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-reportstatus-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-reportstatus-list');
        };

        
        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
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
