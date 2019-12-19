/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("OpexManagementTreeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        OpexManagementTreeEditController]);

    function OpexManagementTreeEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR OPEX';
        vm.view = 'opexmanagementtree-edit-view';
        vm.viewName = 'Opex Management Tree';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.opexManagementTrees = {};
        vm.ccentres = [];
       
   

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var opexmanagementtreeRules = [];

        var setupRules = function () {

            opexmanagementtreeRules.push(new validator.PropertyRule("CostCentreMISCode", {
                required: { message: "CostCentre is required" }
            }));

            opexmanagementtreeRules.push(new validator.PropertyRule("TeamDefinitionCode", {
                required: { message: "TeamDefinition is required" }
            }));

            opexmanagementtreeRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team is required" }
            }));

            
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
               intializeLookUp();

                if ($stateParams.ccdefinitionId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/opexmanagementtree/getopexmanagementtree/' + $stateParams.opexmgtTreeId, null,
                   function (result) {
                       vm.opexManagementTree = result.data;

                       getTeams(vm.opexManagementTree.TeamDefinitionCode);
                       getAccountOfficers(vm.opexManagementTree.AccountOfficerDefinitionCode);

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.opexManagementTree = { CostCentreMISCode: '', TeamDefinitionCode: '', TeamCode: '', AccountOfficerDefinitionCode: '', AccountOfficerCode: '', Rate: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
            getTeamDefinitions();
            getCCentre();
           
        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.opexManagementTree, opexmanagementtreeRules);
            vm.viewModelHelper.modelIsValid = vm.opexManagementTree.isValid;
            vm.viewModelHelper.modelErrors = vm.opexManagementTree.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/opexmanagementtree/updateopexmanagementtree', vm.opexManagementTree,
               function (result) {
                   
                   $state.go('mpr-opexmanagementtree-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.opexManagementTree.errors;

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
                vm.viewModelHelper.apiPost('api/opexmanagementtree/deleteopexmanagementtree', vm.opexManagementTree.OpexMgtTreeId,//vm.opexManagementTree.opexmanagementtreeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-opexmanagementtree-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            }
        }

        vm.cancel = function () {
            $state.go('mpr-opexmanagementtree-list');
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

        var getCCentre = function () {
            vm.viewModelHelper.apiGet('api/costcentre/availablecostCentres', null,
                 function (result) {
                     vm.ccentres = result.data;
                 },
                 function (result) {
                     toastr.error('Fail to load cost centres.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize();
    }
}());
