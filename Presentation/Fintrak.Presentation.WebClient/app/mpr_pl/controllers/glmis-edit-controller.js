/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("GLMISEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        GLMISEditController]);

    function GLMISEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR PL';
        vm.view = 'glmis-edit-view';
        vm.viewName = 'GL Account';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.glMIS = {};
        vm.companies = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var glmisRules = [];

        var setupRules = function () {

            glmisRules.push(new validator.PropertyRule("GLAccount", {
                required: { message: "GL Account is required" }
            }));

            glmisRules.push(new validator.PropertyRule("TeamDefinitionCode", {
                required: { message: "Team Definition is required" }
            }));

            glmisRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team is required" }
            }));

            glmisRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "Account Officer is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.glmisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/glmis/getglmis/' + $stateParams.glmisId, null,
                   function (result) {
                       vm.glMIS = result.data;

                       getTeams(vm.glMIS.TeamDefinitionCode);
                       getAccountOfficers(vm.glMIS.AccountOfficerDefinitionCode);

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.glMIS = { GLAccount: '', TeamDefinitionCode: '', TeamCode: '', AccountOfficerDefinitionCode: '', AccountOfficerCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            getTeamDefinitions();
            getCompanies();
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.glMIS, glmisRules);
            vm.viewModelHelper.modelIsValid = vm.glMIS.isValid;
            vm.viewModelHelper.modelErrors = vm.glMIS.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.glMIS.GlMisId = $stateParams.glmisId;

                vm.viewModelHelper.apiPost('api/glmis/updateglmis', vm.glMIS,
               function (result) {
                   
                   $state.go('mpr-glmis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.glMIS.errors;

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
                vm.viewModelHelper.apiPost('api/glmis/deleteglmis', $stateParams.glmisId,//vm.glMIS.glmisId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-glmis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-glmis-list');
        };

        vm.onTeamDefinitionChanged = function (definition) {
            getTeams(definition);
        }

        vm.onAccountOfficerDefinitionChanged = function (definition) {
            getAccountOfficers(definition);
        }

        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                 function (result) {
                     vm.teamDefinitions = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getTeams = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getAccountOfficers = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                 function (result) {
                     vm.accountOfficers = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var getCompanies = function () {
            vm.viewModelHelper.apiGet('api/company/availablecompanies', null,
                 function (result) {
                     vm.companies = result.data;
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }
        setupRules();
        initialize();
    }
}());
