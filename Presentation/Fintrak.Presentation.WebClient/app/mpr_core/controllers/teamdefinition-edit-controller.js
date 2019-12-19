/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamDefinitionEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamDefinitionEditController]);

    function TeamDefinitionEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamdefinition-edit-view';
        vm.viewName = 'Team Definition';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamDefinition = {};
        vm.fiscalYears = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.periods = [
          { Id: 1 },
          { Id: 2 },
          { Id: 3 },
          { Id: 4 },
          { Id: 5 },
          { Id: 6 },
          { Id: 7 },
          { Id: 8 },
          { Id: 9 },
          { Id: 10 },
          { Id: 11 },
          { Id: 12 }
        ];


        var teamdefinitionRules = [];

        var setupRules = function () {


            teamdefinitionRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            teamdefinitionRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            teamdefinitionRules.push(new validator.PropertyRule("Position", {
                notZero: { message: "Position cannot be zero" }
            }));

            //teamdefinitionRules.push(new validator.PropertyRule("Year", {
            //    required: { message: "Year is required" }
            //}));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.teamdefinitionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamdefinition/getteamdefinition/' + $stateParams.teamdefinitionId, null,
                   function (result) {
                       vm.teamDefinition = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.teamDefinition = { Code: '', Name: '', Position: 0, Year: '', Active: true, Period: 0 };
            }
        }

        var intializeLookUp = function () {
            getFiscalYears();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.teamDefinition, teamdefinitionRules);
            vm.viewModelHelper.modelIsValid = vm.teamDefinition.isValid;
            vm.viewModelHelper.modelErrors = vm.teamDefinition.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/teamdefinition/updateteamdefinition', vm.teamDefinition,
               function (result) {

                   $state.go('mpr-teamdefinition-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.teamDefinition.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete');

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/teamdefinition/deleteteamdefinition', vm.teamDefinition.TeamDefinitionId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-teamdefinition-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-teamdefinition-list');
        };

        var getFiscalYears = function () {
            vm.viewModelHelper.apiGet('api/fiscalyear/availablefiscalYears', null,
                 function (result) {
                     vm.fiscalYears = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load fiscal years.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
