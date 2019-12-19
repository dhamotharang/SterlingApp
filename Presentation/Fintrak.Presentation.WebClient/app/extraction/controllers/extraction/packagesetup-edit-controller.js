/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("PackageSetupEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        PackageSetupEditController]);

    function PackageSetupEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'packagesetup-edit-view';
        vm.viewName = 'Package Setup';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.packageSetup = {};

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var packageSetupRules = [];

        var setupRules = function () {
          
           
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                vm.viewModelHelper.apiGet('api/packageSetup/getpackageSetup', null,
                  function (result) {
                      vm.packageSetup = result.data;

                      if (vm.packageSetup === 'null')
                          vm.packageSetup = { ExtractionPath: '', ProcessPath: '', Active: true };

                      initialView();
                      vm.init === true;
                      
                  },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
            }
        }

        var initialLookUp = function () {
            
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.packageSetup, packageSetupRules);
            vm.viewModelHelper.modelIsValid = vm.packageSetup.isValid;
            vm.viewModelHelper.modelErrors = vm.packageSetup.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/packageSetup/updatepackageSetup', vm.packageSetup,
               function (result) {
                   
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.packageSetup.errors;

                var errorList = '';

                angular.forEach(vm.viewModelHelper.modelErrors, function (error) {
                    errorList += error + '<br>';
                });

                toastr.error(errorList, 'Fintrak');
            }
                
        }

        setupRules();
        initialize(); 
    }
}());
