/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("MPRSetupEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        MPRSetupEditController]);

    function MPRSetupEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'mprsetup-edit-view';
        vm.viewName = 'MPR Setup';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.mprSetup = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        $scope.isDisabled = true;

        vm.poolOption = [
           { Id: 1, Name: 'GrossPool' },
           { Id: 2, Name: 'NetPool' }
        ];

        vm.SwithModes = [
           { Id: 1, Name: 'Team' },
           { Id: 2, Name: 'Team Classification' }
        ];


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

        var mprSetupRules = [];

        var setupRules = function () {


        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                vm.viewModelHelper.apiGet('api/mprsetup/getmprFirstSetup', null,
                  function (result) {
                      vm.mprSetup = result.data[0];

                      setValue();

                      if (vm.mprSetup === 'null')
                          vm.mprSetup = { TeamDefinitionCode: '', ExcoTeamCode: '', AccountLenght: 0, Year: '', CompanyCode: '', Active: true, Period: 0, SwithMode: '', LevelId: '' };

                      var intializeLookUp = function () {

                          getTeamDefinitions();
                      }

                      initialView();
                      vm.init === true;

                  },
                  function (result) {

                  }, null);
            }
        }

        var initialLookUp = function () {

        }

        var initialView = function () {

        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.mprSetup, mprSetupRules);
            vm.viewModelHelper.modelIsValid = vm.mprSetup.isValid;
            vm.viewModelHelper.modelErrors = vm.mprSetup.errors;
            if (vm.viewModelHelper.modelIsValid) {

                vm.viewModelHelper.apiPost('api/mprsetup/updatemprSetup', vm.mprSetup,
               function (result) {

               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else {
                vm.viewModelHelper.modelErrors = vm.mprSetup.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }

        }

        vm.onTeamDefinitionChanged = function (definition) {
            getTeams(definition);
        }


        vm.setValue = function () {
            setValue();
        }

        var setValue = function () {
            if (vm.mprSetup.SwithMode == 'Team') {
                vm.mprSetup.LevelId = 0;
                $scope.isDisabled = true;
            } else {
                $scope.isDisabled = false;
            }
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

        setupRules();
        initialize();
    }
}());
