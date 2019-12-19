/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("BranchDefaultMISEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        BranchDefaultMISEditController]);

    function BranchDefaultMISEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'branchdefaultmis-edit-view';
        vm.viewName = 'Branch Default MIS';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.branchDefaultMIS = {};
        vm.teamDefinitions = [];
        vm.teams = [];
        vm.branches = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var branchdefaultmisRules = [];

        var setupRules = function () {

            branchdefaultmisRules.push(new validator.PropertyRule("BranchCode", {
                required: { message: "Branch is required" }
            }));

            branchdefaultmisRules.push(new validator.PropertyRule("DefinitionCode", {
                required: { message: "Team definition is required" }
            }));

            branchdefaultmisRules.push(new validator.PropertyRule("MisCode", {
                required: { message: "Mis Code is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.branchdefaultmisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/branchdefaultmis/getbranchdefaultmis/' + $stateParams.branchdefaultmisId, null,
                   function (result) {
                       vm.branchDefaultMIS = result.data;

                       getTeams(vm.branchDefaultMIS.DefinitionCode);

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.branchDefaultMIS = { BranchCode:'',DefinitionCode:'',MisCode:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getBranches();
            getTeamDefinitions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.branchDefaultMIS, branchdefaultmisRules);
            vm.viewModelHelper.modelIsValid = vm.branchDefaultMIS.isValid;
            vm.viewModelHelper.modelErrors = vm.branchDefaultMIS.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/branchdefaultmis/updatebranchdefaultmis', vm.branchDefaultMIS,
               function (result) {
                   
                   $state.go('mpr-branchdefaultmis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.branchDefaultMIS.errors;

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
                vm.viewModelHelper.apiPost('api/branchdefaultmis/deletebranchdefaultmis', vm.branchDefaultMIS.BranchDefaultMISId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-branchdefaultmis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-branchdefaultmis-list');
        };

        vm.onTeamDefinitionChanged = function (definition) {
            getTeams(definition);
        }

        var getBranches = function () {
            vm.viewModelHelper.apiGet('api/branch/availablebranches', null,
                 function (result) {
                     vm.branches = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
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

        setupRules();
        initialize(); 
    }
}());
