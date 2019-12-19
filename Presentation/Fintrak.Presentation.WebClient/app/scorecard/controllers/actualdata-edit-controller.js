/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCDActualDataEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SCDActualDataEditController]);

    function SCDActualDataEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'actualdata-edit-view';
        vm.viewName = 'Actual Data';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.actual = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var actualRules = [];

        var setupRules = function () {
          
            actualRules.push(new validator.PropertyRule("Caption", {
                required: { message: "Caption is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups actualId
                intializeLookUp();
                if ($stateParams.actualdataId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/actual/getactual/' + $stateParams.actualdataId, null,
                   function (result) {
                       vm.actual = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.actual = { Caption: '', Amount: 0,Date:new Date(), Period:1, Year: '',Active: true };
            }
        }


        var intializeLookUp = function () {
          
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.actual, actualRules);
            vm.viewModelHelper.modelIsValid = vm.actual.isValid;
            vm.viewModelHelper.modelErrors = vm.actual.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/actual/updateactual', vm.actual,
               function (result) {
                   
                   $state.go('scd-actualdata-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.actual.errors;

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
                vm.viewModelHelper.apiPost('api/actual/deleteactual', vm.actual.ActualId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-actualdata-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-actualdata-list');
        };

        vm.openDate = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            vm.openedDate = true;
        }

        setupRules();
        initialize(); 
    }
}());
