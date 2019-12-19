/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GeneralTransferPriceEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GeneralTransferPriceEditController]);

    function GeneralTransferPriceEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'generaltransferprice-edit-view';
        vm.viewName = 'General Transfer Price';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.generalTransferPrice = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.category = [
         { Id: 2, Name: 'Asset' },
         { Id: 3, Name: 'Liability' }
        ];

        vm.currencyTypes = [
         { Id: 1, Name: 'LCY' },
         { Id: 2, Name: 'FCY' }
        ];

        var generaltransferpriceRules = [];

        vm.solutions = [];

        var setupRules = function () {

            
            generaltransferpriceRules.push(new validator.PropertyRule("Category", {
                notZero: { message: "Category is required" }
            }));

            generaltransferpriceRules.push(new validator.PropertyRule("CurrencyType", {
                notZero: { message: "CurrencyType is required" }
            }));

            generaltransferpriceRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            generaltransferpriceRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

           
        }



        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                 intializeLookUp();

                if ($stateParams.generaltransferpriceId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/generaltransferprice/getgeneraltransferprice/' + $stateParams.generaltransferpriceId, null,
                   function (result) {
                       vm.generalTransferPrice = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {

                   }, null);
                }
                else
                    vm.generalTransferPrice = { AccountNo: '', Category: '', Rate: 0, Year: 0, Period: 0, SolutionId: '', Active: true };
            }
        }

        var initialView = function () {

        }

        var intializeLookUp = function () {
            getSolutions();
            getDefinitions();
            // getLoanProducts();
            //getScheduleTypes();
        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.generalTransferPrice, generaltransferpriceRules);
            vm.viewModelHelper.modelIsValid = vm.generalTransferPrice.isValid;
            vm.viewModelHelper.modelErrors = vm.generalTransferPrice.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/generaltransferprice/updategeneraltransferprice', vm.generalTransferPrice,
               function (result) {
                   //
                   $state.go('mpr-generaltransferprice-list');
               },
               function (result) {

               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.generalTransferPrice.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });
            }

        }
        // vm.derivedCaption.DerivedCaptionId,
        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/generaltransferprice/deletegeneraltransferprice', vm.generalTransferPrice.GeneralTransferPriceId,//vm.generalTransferPrice.generaltransferpriceId,
              function (result) {
                  //
                  $state.go('mpr-generaltransferprice-list');
              },
              function (result) {

              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-generaltransferprice-list');
        };


        var getSolutions = function () {
            vm.viewModelHelper.apiGet('api/solution/availablesolutions', null,
                 function (result) {
                     vm.solutions = result.data;
                 },
                 function (result) {

                 }, null);
        }

        vm.onDefinitionChanged = function (definition) {
            getTeams(definition);
        }

        var getDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                 function (result) {
                     vm.definitions = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load definitions.', 'Fintrak');
                 }, null);
        }

        var getTeams = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load teams.', 'Fintrak');
                 }, null);
        }


        setupRules();
        initialize();
    }
}());
