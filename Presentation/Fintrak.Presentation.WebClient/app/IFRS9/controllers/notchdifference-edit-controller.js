/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("NotchDifferenceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        NotchDifferenceEditController]);

    function NotchDifferenceEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'notchdifference-edit-view';
        vm.viewName = 'Notch Difference';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.notchDifference = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var notchdifferenceRules = [];


        vm.initialclassifications = [
            { Id: 1, Name: 'Non-Performing'},
            { Id: 2, Name: 'Performing' },
            { Id: 3, Name: 'Under-Performing' },
        ];

        vm.finalclassifications = [
            { Id: 1, Name: 'Non-Performing' },
            { Id: 2, Name: 'Performing' },
            { Id: 3, Name: 'Under-Performing' },
        ];


        var setupRules = function () {
          
            notchdifferenceRules.push(new validator.PropertyRule("InitialClassification", {
                required: { message: "Initial Classification is required" }
            }));

            notchdifferenceRules.push(new validator.PropertyRule("StepMovement", {
                required: { message: "Step Movement is required" }
            }));

            notchdifferenceRules.push(new validator.PropertyRule("FinalClassification", {
                required: { message: "Final Classification is required" }
            }));
        
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.notchdifferenceId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/notchdifference/getnotchdifference/' + $stateParams.notchdifferenceId, null,
                   function (result) {
                       vm.notchDifference = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.notchDifference = { InitialClassification: '', StepMovement: 0, FinalClassification: '', Active: true };
            }
        }

        var intializeLookUp = function () {
                      
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.notchDifference, notchdifferenceRules);
            vm.viewModelHelper.modelIsValid = vm.notchDifference.isValid;
            vm.viewModelHelper.modelErrors = vm.notchDifference.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/notchdifference/updatenotchdifference', vm.notchDifference,
               function (result) {
                   
                   $state.go('ifrs9-notchdifference-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.notchDifference.errors;

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
                vm.viewModelHelper.apiPost('api/notchdifference/deletenotchdifference', vm.notchDifference.NotchDifferenceId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-notchdifference-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-notchdifference-list');
        };


        setupRules();
        initialize(); 
    }
}());
