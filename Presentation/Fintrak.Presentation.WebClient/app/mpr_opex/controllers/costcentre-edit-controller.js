/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CostCentreEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CostCentreEditController]);

    function CostCentreEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'costcentre-edit-view';
        vm.viewName = 'Cost Centre';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.costCentres = {};
        vm.parents = [];
        vm.CCDefintions = [];

       
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var costcentreRules = [];

        var setupRules = function () {

            costcentreRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            costcentreRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            costcentreRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "CostCentreDefinitionName is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.costcentreId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/costcentre/getcostcentre/' + $stateParams.costcentreId, null,
                   function (result) {
                       vm.costCentre = result.data;
                       //getCCDefintion();
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.costCentre = { Code: '', Name: '', DefinitionCode: 0, Parent: '', Year: '', Active: true };
            }
        }


        var intializeLookUp = function () {          
            getName();
            getCCDefintion();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.costCentre, costcentreRules);
            vm.viewModelHelper.modelIsValid = vm.costCentre.isValid;
            vm.viewModelHelper.modelErrors = vm.costCentre.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/costcentre/updatecostcentre', vm.costCentre,
               function (result) {
                   
                   $state.go('mpr-costcentre-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.costCentre.errors;

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
                vm.viewModelHelper.apiPost('api/costcentre/deletecostcentre', vm.costCentre.CostCentreId,//vm.costCentre.costcentreId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-costcentre-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-costcentre-list');
        };

        var getName = function () {
            vm.viewModelHelper.apiGet('api/costcentre/availablecostCentres', null,
                 function (result) {
                     vm.parents = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load cost centres.', 'Fintrak');
                 }, null);
        }


        var getCCDefintion = function () {
            vm.viewModelHelper.apiGet('api/costcentredefinition/availablecostCentreDefinitions', null,
                 function (result) {
                     vm.CCDefintions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load definitions.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
