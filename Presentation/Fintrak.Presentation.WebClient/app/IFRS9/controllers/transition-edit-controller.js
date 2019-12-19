/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TransitionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TransitionEditController]);

    function TransitionEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'transition-edit-view';
        vm.viewName = 'Transition';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.transition = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var transitionRules = [];

        vm.prudentialclassifications = [
            { Id: 1, Name: 'Doubtful' },
            { Id: 2, Name: 'Lost' },
            { Id: 3, Name: 'Standard' },
            { Id: 4, Name: 'Sub-Standard' },
        ];

        vm.ifrs9classifications = [
            { Id: 1, Name: 'Non-Performing' },
            { Id: 2, Name: 'Performing' },
             { Id: 3, Name: 'Under-Performing' },
        ];

        var setupRules = function () {
          
            transitionRules.push(new validator.PropertyRule("Prudential_Classification", {
                required: { message: "Prudential Classification is required" }
            }));

            transitionRules.push(new validator.PropertyRule("PDD_LowerBoundary", {
                required: { message: "PDD LowerBoundary is required" }
            }));

            transitionRules.push(new validator.PropertyRule("PDD_UpperBoundary", {
                required: { message: "PDD UpperBoundary is required" }
            }));

            transitionRules.push(new validator.PropertyRule("IFRS9_Classification", {
                required: { message: "IFRS9 Classification is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.transitionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/transition/gettransition/' + $stateParams.transitionId, null,
                   function (result) {
                       vm.transition = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.transition = { Prudential_Classification: '', PDD_LowerBoundary: 0, PDD_UpperBoundary: 0, IFRS9_Classification: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.transition, transitionRules);
            vm.viewModelHelper.modelIsValid = vm.transition.isValid;
            vm.viewModelHelper.modelErrors = vm.transition.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/transition/updatetransition', vm.transition,
               function (result) {
                   
                   $state.go('ifrs9-transition-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.transition.errors;

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
                vm.viewModelHelper.apiPost('api/transition/deletetransition', vm.transition.TransitionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-transition-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-transition-list');
        };

        setupRules();
        initialize(); 
    }
}());
