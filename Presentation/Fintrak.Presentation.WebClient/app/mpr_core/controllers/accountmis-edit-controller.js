/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("AccountMISEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        AccountMISEditController]);

    function AccountMISEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'accountmis-edit-view';
        vm.viewName = 'Account MIS';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.accountMIS = {};

     
      
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var accountmisRules = [];

        var setupRules = function () {
          
            accountmisRules.push(new validator.PropertyRule("AccountOfficerDefinitionCode", {
                required: { message: "AccountOfficerDefinitionCode is required" }
            }));

            accountmisRules.push(new validator.PropertyRule("AccountOfficerCode", {
                required: { message: "AccountOfficerCode is required" }
            }));

            accountmisRules.push(new validator.PropertyRule("TeamDefinitionCode", {
                required: { message: "Team Definition is required" }
            }));

            accountmisRules.push(new validator.PropertyRule("TeamCode", {
                required: { message: "Team is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.accountmisId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/accountmis/getaccountmis/' + $stateParams.accountmisId, null,
                   function (result) {
                       vm.accountMIS = result.data;

                       getTeams(vm.accountMIS.TeamDefinitionCode);
                       getAccountOfficers(vm.accountMIS.AccountOfficerDefinitionCode);

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.accountMIS = { ProductCode: '', CaptionCode: '', TeamDefinitionCode: '', TeamCode: '',AccountOfficerDefinitionCode: '', AccountOfficerCode: '', Active: true };
            }
        }

        var intializeLookUp = function () {
         
            getTeamDefinitions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.accountMIS, accountmisRules);
            vm.viewModelHelper.modelIsValid = vm.accountMIS.isValid;
            vm.viewModelHelper.modelErrors = vm.accountMIS.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/accountmis/updateaccountmis', vm.accountMIS,
               function (result) {
                   
                   $state.go('mpr-accountmis-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.accountMIS.errors;

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
                vm.viewModelHelper.apiPost('api/accountmis/deleteaccountmis', vm.accountMIS.AccountMISId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('mpr-accountmis-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-accountmis-list');
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
