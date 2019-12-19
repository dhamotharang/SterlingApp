/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ETLConfigurationEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ETLConfigurationEditController]);

    function ETLConfigurationEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'CDQM';
        vm.view = 'etlconfiguration-edit-view';
        vm.viewName = 'ETL Configuration';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.etlConfiguration = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var etlConfigurationRules = [];

        var setupRules = function () {
          
            
            //etlConfigurationRules.push(new validator.PropertyRule("StreetName", {
            //    required: { message: "Street Name is required" }
            //}));

          

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.etlconfigurationId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/cdqmetlConfiguration/getcdqmetlConfiguration/' + $stateParams.etlconfigurationId, null,
                   function (result) {
                       vm.etlConfiguration = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.etlConfiguration = { ConfigurationFilter: '', ConfigurationValue: '',PackagePath:'',ConfiguredValueType:'', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.etlConfiguration, etlConfigurationRules);
            vm.viewModelHelper.modelIsValid = vm.etlConfiguration.isValid;
            vm.viewModelHelper.modelErrors = vm.etlConfiguration.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/cdqmetlConfiguration/updatecdqmetlConfiguration', vm.etlConfiguration,
               function (result) {
                   
                   $state.go('cdqm-etlconfiguration-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.etlConfiguration.errors;

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
                vm.viewModelHelper.apiPost('api/cdqmetlConfiguration/deletecdqmetlConfiguration', vm.etlConfiguration.ETLConfigurationId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('cdqm-etlconfiguration-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('cdqm-etlconfiguration-list');
        };

        setupRules();
        initialize(); 
    }
}());
