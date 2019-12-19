/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamClassificationMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamClassificationMapEditController]);

    function TeamClassificationMapEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamclassificationmap-edit-view';
        vm.viewName = 'Classification Map';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamClassificationMap = {};
        vm.teamClassifications = [];

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

        var teamclassificationmapRules = [];

        var setupRules = function () {

            teamclassificationmapRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "Mis Code is required" }
            }));

            teamclassificationmapRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Definition Code is required" }
            }));

            teamclassificationmapRules.push(new validator.PropertyRule("ClassificationCode", {
                required: { message: "Classification is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.teamclassificationmapId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamclassificationmap/getteamclassificationmap/' + $stateParams.teamclassificationmapId, null,
                   function (result) {
                       vm.teamClassificationMap = result.data;
                       getClassifications(vm.teamClassificationMap.ClassificationTypeCode);

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       vm.teamClassificationMap = { ClassificationTypeCode: '', ClassificationCode: '', MisCode: $stateParams.miscode, DefinitionCode: $stateParams.definitioncode, Active: true };
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.teamClassificationMap = { ClassificationTypeCode: '', ClassificationCode: '', MisCode: $stateParams.miscode, DefinitionCode: $stateParams.definitioncode, Active: true };
            }
        }

        var intializeLookUp = function () {
            getClassificationTypes();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.teamClassificationMap, teamclassificationmapRules);
            vm.viewModelHelper.modelIsValid = vm.teamClassificationMap.isValid;
            vm.viewModelHelper.modelErrors = vm.teamClassificationMap.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/teamclassificationmap/updateteamclassificationmap', vm.teamClassificationMap,
               function (result) {

                   $state.go('mpr-team-edit', { teamId: $stateParams.teamId });
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.teamClassificationMap.errors;

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
                vm.viewModelHelper.apiPost('api/teamclassificationmap/deleteteamclassificationmap', vm.teamClassificationMap.TeamClassificationMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-team-edit', { teamId: $stateParams.teamId });
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-team-edit', { teamId: $stateParams.teamId });
        };

        vm.onClassificationTypeChanged = function (typeCode) {
            getClassifications(typeCode);
        }

        var getClassificationTypes = function () {
            vm.viewModelHelper.apiGet('api/teamClassificationType/availableteamClassificationTypes', null,
                 function (result) {
                     vm.teamClassificationTypes = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getClassifications = function (typeCode) {
            vm.viewModelHelper.apiGet('api/teamclassification/getActiveClassifications/' + typeCode, null,
                 function (result) {
                     vm.teamClassifications = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load classifications.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
