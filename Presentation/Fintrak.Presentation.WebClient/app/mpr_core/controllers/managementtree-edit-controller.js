/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ManagementTreeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ManagementTreeEditController]);

    function ManagementTreeEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'managementtree-edit-view';
        vm.viewName = 'Management Tree';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.managementTree = {};

        //vm.products = [];
        //vm.captions = [];
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var managementtreeRules = [];

        var setupRules = function () {
          
            managementtreeRules.push(new validator.PropertyRule("AccountOfficerDefinitionCode", {
                required: { message: "AccountOfficerDefinitionCode is required" }
            }));

            managementtreeRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "AccountOfficerCode is required" }
            }));

            managementtreeRules.push(new validator.PropertyRule("TeamDefinitionCode", {
                required: { message: "Team Definition is required" }
            }));

            managementtreeRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.managementtreeId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/managementtree/getmanagementtree/' + $stateParams.managementtreeId, null,
                   function (result) {
                       vm.managementTree = result.data;

                       getTeams(vm.managementTree.TeamDefinitionCode);
                       getAccountOfficers(vm.managementTree.AccountOfficerDefinitionCode);

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.managementTree = { ProductCode: '', CaptionCode: '', TeamDefinitionCode: '', TeamCode: '',AccountOfficerDefinitionCode: '', AccountOfficerCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
            //getProducts();
            //getCaptions();
            getTeamDefinitions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.managementTree, managementtreeRules);
            vm.viewModelHelper.modelIsValid = vm.managementTree.isValid;
            vm.viewModelHelper.modelErrors = vm.managementTree.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/managementtree/updatemanagementtree', vm.managementTree,
               function (result) {
                   
                   $state.go('mpr-managementtree-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.managementTree.errors;

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
                vm.viewModelHelper.apiPost('api/managementtree/deletemanagementtree', vm.managementTree.ManagementTreeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-managementtree-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-managementtree-list');
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

        setupRules();
        initialize(); 
    }
}());
