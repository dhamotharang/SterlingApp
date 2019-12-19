/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamClassificationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamClassificationEditController]);

    function TeamClassificationEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'teamclassification-edit-view';
        vm.viewName = 'Classification';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamClassification = {};
        vm.teamClassificationTypes = [];

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

        var teamclassificationRules = [];

        var setupRules = function () {


            teamclassificationRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));

            teamclassificationRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            teamclassificationRules.push(new validator.PropertyRule("ClassificationTypeCode", {
                required: { message: "Classification Type is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.teamclassificationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamclassification/getteamclassification/' + $stateParams.teamclassificationId, null,
                   function (result) {
                       vm.teamClassification = result.data;

                       initialView();
                       vm.init === true;

                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.teamClassification = { Name: '', Code: '', ClassificationTypeCode: '', Active: true, Period: 0 };
            }
        }

        var intializeLookUp = function () {
            getClassificationTypes();
            getParents();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.teamClassification, teamclassificationRules);
            vm.viewModelHelper.modelIsValid = vm.teamClassification.isValid;
            vm.viewModelHelper.modelErrors = vm.teamClassification.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/teamclassification/updateteamclassification', vm.teamClassification,
               function (result) {

                   $state.go('mpr-teamclassification-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.teamClassification.errors;

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
                vm.viewModelHelper.apiPost('api/teamclassification/deleteteamclassification', vm.teamClassification.TeamClassificationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-teamclassification-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-teamclassification-list');
        };

        var getClassificationTypes = function () {
            vm.viewModelHelper.apiGet('api/teamClassificationType/availableteamClassificationTypes', null,
                 function (result) {
                     vm.teamClassificationTypes = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getParents = function () {
            vm.viewModelHelper.apiGet('api/teamClassification/availableteamClassifications', null,
                  function (result) {
                      vm.parents = result.data;
                  },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        setupRules();
        initialize();
    }
}());
