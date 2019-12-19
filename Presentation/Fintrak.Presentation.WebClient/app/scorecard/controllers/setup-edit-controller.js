/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SCDSetupEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SCDSetupEditController]);

    function SCDSetupEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Scorecard';
        vm.view = 'setup-edit-view';
        vm.viewName = 'Scorecard Setup';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.setup = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.operationModes = [
            { Id: 1, Name: 'Upload Only' },
            { Id: 2, Name: 'External Only' },
            { Id: 3, Name: 'Hybrid' }
        ];

        vm.periodTypes = [
           { Id: 1, Name: 'Daily' },
           { Id: 2, Name: 'Weekly' },
           { Id: 3, Name: 'Monthly' },
           { Id: 4, Name: 'Quarterly' },
           { Id: 5, Name: 'Yearly' }
        ];

        var setupRules = function () {
          
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                vm.viewModelHelper.apiGet('api/scdconfiguration/getconfiguration', null,
                  function (result) {
                      vm.setup = result.data;

                      if (vm.setup === 'null')
                          vm.setup = { Mode: 1, PeriodType: 1, TeamClassificationTypeCode: '', CompanyCode: '', Active: true };

                      initialView();
                      vm.init === true;

                  },
                  function (result) {

                  }, null); 
            }
        }

        var initialLookUp = function () {
            getTeamClassificationTypes();
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.setup, setupRules);
            vm.viewModelHelper.modelIsValid = vm.setup.isValid;
            vm.viewModelHelper.modelErrors = vm.setup.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/scdconfiguration/updateconfiguration', vm.setup,
               function (result) {
                   
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.setup.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

         var getTeamClassificationTypes = function () {
             vm.viewModelHelper.apiGet('api/teamclassificationtype/availableteamClassificationTypes', null,
                 function (result) {
                     vm.classificationTypes = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
