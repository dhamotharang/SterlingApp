/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MacrovariableEstimateEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MacrovariableEstimateEditController]);

    function MacrovariableEstimateEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'macrovariableestimate-edit-view';
        vm.viewName = 'Probability Weigthing for ECL Scenario'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.macrovariableEstimate = {};
        vm.selectedOption = "";
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.categories = [
          { Id: 1, Name: 'Retail' },
          { Id: 2, Name: 'Corporate' },
        ];

        var macrovariableEstimateRules = [];

        var setupRules = function () {

            macrovariableEstimateRules.push(new validator.PropertyRule("Seq", {
                required: { message: "Optimistic is required" }
            }));
          
            macrovariableEstimateRules.push(new validator.PropertyRule("Optimistic", {
                required: { message: "Optimistic is required" }
            }));


            macrovariableEstimateRules.push(new validator.PropertyRule("Best", {
                required: { message: "Best Estimate is required" }
            }));


            macrovariableEstimateRules.push(new validator.PropertyRule("Downturn", {
                required: { message: "Downturn is required" }
            }));


            macrovariableEstimateRules.push(new validator.PropertyRule("Category", {
                required: { message: "Category is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.MacrovariableEstimate_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/macrovariableestimate/getmacrovariableestimate/' + $stateParams.MacrovariableEstimate_Id, null,
                   function (result) {
                       vm.macrovariableEstimate = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.macrovariableEstimate = {Seq:'', Optimistic: '', Best: '', Downturn: '', Date:'', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.macrovariableEstimate, macrovariableEstimateRules);
            vm.viewModelHelper.modelIsValid = vm.macrovariableEstimate.isValid;
            vm.viewModelHelper.modelErrors = vm.macrovariableEstimate.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/macrovariableestimate/updatemacrovariableestimate', vm.macrovariableEstimate,
               function (result) {
                   
                   $state.go('ifrs9-macrovariableestimate-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.macrovariableEstimate.errors;

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
                vm.viewModelHelper.apiPost('api/macrovariableestimate/deletemacrovariableestimate', vm.macrovariableEstimate.MacrovariableEstimate_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-macrovariableestimate-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-macrovariableestimate-list');
        };


        setupRules();
        initialize(); 
    }
}());
