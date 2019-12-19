/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ClosedPeriodTemplateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ClosedPeriodTemplateEditController]);

    function ClosedPeriodTemplateEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Extraction & Process';
        vm.view = 'closedperiodtemplate-edit-view';
        vm.viewName = 'Closed Period Template';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.closedPeriodTemplate = {};

        vm.solutions = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var closedPeriodRules = [];

        var setupRules = function () {
          
            closedPeriodRules.push(new validator.PropertyRule("SolutionId", {
                notZero: { message: "Solution is required" }
            }));

            closedPeriodRules.push(new validator.PropertyRule("Action", {
                required: { message: "Action is require" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intialiazeLookUp();

                if ($stateParams.closedperiodtemplateId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/closedPeriodtemplate/getclosedperiodtemplate/' + $stateParams.closedperiodtemplateId, null,
                   function (result) {
                       vm.closedPeriodTemplate = result.data;

                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.closedPeriodTemplate = { SolutionId: 0, Action: '', Active: true };
            }
        }

        var intialiazeLookUp = function () {
            getSolutions();
        }

        var initialView = function () {
        
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.closedPeriodTemplate, closedPeriodRules);
            vm.viewModelHelper.modelIsValid = vm.closedPeriodTemplate.isValid;
            vm.viewModelHelper.modelErrors = vm.closedPeriodTemplate.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/closedPeriodtemplate/updateclosedPeriodtemplate', vm.closedPeriodTemplate,
               function (result) {
                   
                   $state.go('core-closedperiodtemplate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.closedPeriodTemplate.errors;

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
                vm.viewModelHelper.apiPost('api/closedPeriodtemplate/deleteclosedPeriodtemplate', vm.closedPeriodTemplate.ClosedPeriodTemplateId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-closedperiodtemplate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-closedperiodtemplate-list');
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
