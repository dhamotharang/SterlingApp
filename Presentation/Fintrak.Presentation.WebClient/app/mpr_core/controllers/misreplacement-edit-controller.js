/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MISReplacementEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MISReplacementEditController]);

    function MISReplacementEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'misreplacement-edit-view';
        vm.viewName = 'MIS Replacement';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.misReplacement = {};
        vm.teamDefinitions = [];
        vm.teams = [];
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var misreplacementRules = [];

        var setupRules = function () {

            misreplacementRules.push(new validator.PropertyRule("OldMISCode", {
                required: { message: "Old MIS code is required" }
            }));

            misreplacementRules.push(new validator.PropertyRule("TeamDefinitionId", {
                notZero: { message: "Team definition is required" }
            }));

            misreplacementRules.push(new validator.PropertyRule("MISCode", {
                notZero: { message: "Replacement MIS Code is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.misreplacementId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/misreplacement/getmisreplacement/' + $stateParams.misreplacementId, null,
                   function (result) {
                       vm.misReplacement = result.data;

                       getTeams(vm.misReplacement.DefinitionCode);

                       initialView();
                       vm.init === true;
                     
                   },
                   function (result) {
                     
                   }, null);
               }
               else
                    vm.misReplacement = { OldMIScode:'',DefinitionCode:'',MISCode:'', Active: true };
            }
        }

        var intializeLookUp = function () {
            getTeamDefinitions();
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.misReplacement, misreplacementRules);
            vm.viewModelHelper.modelIsValid = vm.misReplacement.isValid;
            vm.viewModelHelper.modelErrors = vm.misReplacement.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/misreplacement/updatemisreplacement', vm.misReplacement,
               function (result) {
                 //
                   $state.go('mpr-misreplacement-list');
               },
               function (result) {
                  
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.misReplacement.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                }); 
            }
                
        }

        vm.delete = function () {
            var deleteFlag = $window.confirm(' Are you sure you want to delete' );

            if (deleteFlag) {
                vm.viewModelHelper.apiPost('api/misreplacement/deletemisreplacement', vm.misReplacement.MISReplacementId,
              function (result) {
                 //
                  $state.go('mpr-misreplacement-list');
              },
              function (result) {
                 
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('mpr-misreplacement-list');
        };

        vm.onTeamDefinitionChanged = function (definition) {
            getTeams(definition);
        }

        var getTeamDefinitions = function () {
            vm.viewModelHelper.apiGet('api/teamdefinition/availableteamdefinitions', null,
                 function (result) {
                     vm.teamDefinitions = result.data;
                 },
                 function (result) {

                 }, null);
        }

        var getTeams = function (definition) {
            vm.viewModelHelper.apiGet('api/team/getteambydefinition/' + definition, null,
                 function (result) {
                     vm.teams = result.data;
                 },
                 function (result) {

                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
