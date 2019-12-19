/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CostCentreDefinitionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CostCentreDefinitionEditController]);

    function CostCentreDefinitionEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'costcentredefinition-edit-view';
        vm.viewName = 'Cost Centre Definition';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.costCentreDefinitions = {};
       
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var costcentredefinitionRules = [];

        var setupRules = function () {

            costcentredefinitionRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            costcentredefinitionRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            costcentredefinitionRules.push(new validator.PropertyRule("Position", {
                required: { message: "Position is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               // intializeLookUp();

                if ($stateParams.ccdefinitionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/costcentredefinition/getcostcentredefinition/' + $stateParams.ccdefinitionId, null,
                   function (result) {
                       vm.costCentreDefinition = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.costCentreDefinition = { Code: '', Name: '', Position: 0 , Year: '', Active: true };
            }
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.costCentreDefinition, costcentredefinitionRules);
            vm.viewModelHelper.modelIsValid = vm.costCentreDefinition.isValid;
            vm.viewModelHelper.modelErrors = vm.costCentreDefinition.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/costcentredefinition/updatecostcentredefinition', vm.costCentreDefinition,
               function (result) {
                   
                   $state.go('mpr-costcentredefinition-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.costCentreDefinition.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/costcentredefinition/deletecostcentredefinition', vm.costCentreDefinition.CCDefinitionId,//vm.costCentreDefinition.costcentredefinitionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-costcentredefinition-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-costcentredefinition-list');
        };

        setupRules();
        initialize();
    }
}());
