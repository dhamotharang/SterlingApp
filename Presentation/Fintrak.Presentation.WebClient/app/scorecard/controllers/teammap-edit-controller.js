/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("TeamMapEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        TeamMapEditController]);

    function TeamMapEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'teammap-edit-view';
        vm.viewName = 'KPI Collection To User Mapping';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamMap = {};

        vm.definitions = [];
        vm.teams = [];
        vm.staffs = [];
        vm.grades = [];
        vm.classifications = [];

        vm.officeTypes = [
            { Id: 1, Name: 'Cost Centre' },
            { Id: 2, Name: 'Profit Centre' }
        ];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var teamMapRules = [];

        var setupRules = function () {
          
            teamMapRules.push(new validator.PropertyRule("Centre", {
                notZero: { message: "Centre is required" }
            }));

            teamMapRules.push(new validator.PropertyRule("TeamDefinitionCode", {
                required: { message: "Team Definition Code is required" }
            }));

            teamMapRules.push(new validator.PropertyRule("MISCode", {
                required: { message: "MISCode is required" }
            }));

            teamMapRules.push(new validator.PropertyRule("MISName", {
                required: { message: "MISName is required" }
            }));

            teamMapRules.push(new validator.PropertyRule("StaffCode", {
                required: { message: "StaffCode is required" }
            }));

            teamMapRules.push(new validator.PropertyRule("Grade", {
                required: { message: "Grade is required" }
            }));

            //teamMapRules.push(new validator.PropertyRule("ParentCode", {
            //    required: { message: "ParentCode is required" }
            //}));

            teamMapRules.push(new validator.PropertyRule("Period", {
                required: { message: "Period is required" }
            }));

            teamMapRules.push(new validator.PropertyRule("Year", {
                required: { message: "Year is required" }
            }));

            teamMapRules.push(new validator.PropertyRule("TeamClassificationCode", {
                required: { message: "Classification is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.teammapId !== 0 && $stateParams.teammapId !=="0") {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/teamMap/getteamMap/' + $stateParams.teammapId, null,
                   function (result) {
                       vm.teamMap = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.teamMap = { Centre: 1, TeamDefinitionCode: '',MISCode:'',MISName:'',StaffCode:'',Grade:'',ParentCode:'',Period:'',Year:'',TeamClassificationCode:'', Active: true };
            }
        }


        var intializeLookUp = function () {
            getStaffs();
            getGrades();
            getClassifications();
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.teamMap, teamMapRules);
            vm.viewModelHelper.modelIsValid = vm.teamMap.isValid;
            vm.viewModelHelper.modelErrors = vm.teamMap.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/teamMap/updateteamMap', vm.teamMap,
               function (result) {
                   
                   $state.go('scd-teammapping-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.teamMap.errors;

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
                vm.viewModelHelper.apiPost('api/teamMap/deleteteamMap', vm.teamMap.TeamMapId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-teammapping-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-teammapping-list');
        };


        var getStaffs = function () {
            vm.viewModelHelper.apiGet('api/staff/availablestaffs', null,
                 function (result) {
                     vm.staffs = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load staffs.', 'Fintrak');
                 }, null);
        }

        var getGrades = function () {
            vm.viewModelHelper.apiGet('api/paygrade/availablepaygrades', null,
                 function (result) {
                     vm.grades = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load grades.', 'Fintrak');
                 }, null);
        }

        var getClassifications = function (centre) {
            vm.viewModelHelper.apiGet('api/scdteamclassification/availableteamClassification', null,
                   function (result) {
                       vm.classifications = result.data;

                   },
                   function (result) {
                       toastr.error('Fail to load team classification.', 'Fintrak');
                   }, null);
        }

        setupRules();
        initialize(); 
    }
}());
