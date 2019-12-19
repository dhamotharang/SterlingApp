/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamClassificationTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamClassificationTypeEditController]);

    function TeamClassificationTypeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'teamclassificationtype-edit-view';
        vm.viewName = 'Classification Type';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamClassificationType = {};
        vm.fiscalYears = [];
        vm.companies = [];


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


        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var teamclassificationtypeRules = [];

        var setupRules = function () {

            teamclassificationtypeRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            teamclassificationtypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            teamclassificationtypeRules.push(new validator.PropertyRule("MaximumLevel", {
                required: { message: "MaximumLevel is required" }
            }));

            teamclassificationtypeRules.push(new validator.PropertyRule("CompanyCode", {
                required: { message: "CompanyCode is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.teamclassificationtypeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamclassificationtype/getteamClassificationType/' + $stateParams.teamclassificationtypeId, null,
                   function (result) {
                       vm.teamClassificationType = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.teamClassificationType = { Code: '', Name: '', MaximumLevel: 0, CompanyCode: '', Active: true};
            }
        }

        var intializeLookUp = function () {
            getFiscalYears();
            getCompanies()
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.teamClassificationType, teamclassificationtypeRules);
            vm.viewModelHelper.modelIsValid = vm.teamClassificationType.isValid;
            vm.viewModelHelper.modelErrors = vm.teamClassificationType.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/teamclassificationtype/updateteamclassificationtype', vm.teamClassificationType,
               function (result) {

                   $state.go('mpr-teamclassificationtype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.teamClassificationType.errors;

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
                vm.viewModelHelper.apiPost('api/teamclassificationtype/deleteteamclassificationtype', vm.teamClassificationType.TeamClassificationTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-teamclassificationtype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-teamclassificationtype-list');
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

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Unable to load companies', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
