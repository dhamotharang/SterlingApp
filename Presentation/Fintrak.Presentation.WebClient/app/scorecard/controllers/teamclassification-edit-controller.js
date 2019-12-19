/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCDTeamClassificationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SCDTeamClassificationEditController]);

    function SCDTeamClassificationEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'teamclassification-edit-view';
        vm.viewName = 'KPI Collection';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.teamClassification = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';


        var teamClassificationRules = [];

        var setupRules = function () {
          
            teamClassificationRules.push(new validator.PropertyRule("Code", {
                required: { message: "Code is required" }
            }));

            teamClassificationRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
          
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();
                if ($stateParams.teamclassificationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/scdteamclassification/getteamclassification/' + $stateParams.teamclassificationId, null,
                   function (result) {
                       vm.teamClassification = result.data;

                       initialView();
                       vm.init = true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.teamClassification = { Code: '', Name: '',Active: true };
            }
        }


        var intializeLookUp = function () {
          
        }

        var initialView = function () {

        }


        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.teamClassification, teamClassificationRules);
            vm.viewModelHelper.modelIsValid = vm.teamClassification.isValid;
            vm.viewModelHelper.modelErrors = vm.teamClassification.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/scdteamclassification/updateteamclassification', vm.teamClassification,
               function (result) {
                   
                   $state.go('scd-teamclassification-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.teamClassification.errors;

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
                vm.viewModelHelper.apiPost('api/teamClassification/deleteteamClassification', vm.teamClassification.TeamClassificationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('scd-teamclassification-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('scd-teamclassification-list');
        };

       
        setupRules();
        initialize(); 
    }
}());
