/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("SICRParametersEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        SICRParametersEditController]);

    function SICRParametersEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'sicrparameters-edit-view';
        vm.viewName = 'SICR Parameters';
        vm.showChildren=false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.sicrParameters = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
       
        var sicrparametersRules = [];

        var setupRules = function () {
          
            sicrparametersRules.push(new validator.PropertyRule("SICR_Param", {
                required: { message: "SICR Parameter required" }
            }));

            sicrparametersRules.push(new validator.PropertyRule("SICR_Desc", {
                required: { message: "SICR Description required" }
            }));
        }
       
        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ID !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/sicrparameters/getsicrparameters/' + $stateParams.ID, null,
                   function (result) {
                       vm.sicrParameters = result.data;                       
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.sicrParameters = { Threshold: 0, Deteroriation_Level: 0, Classification_Type: 0, Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.sicrParameters, sicrparametersRules);
            vm.viewModelHelper.modelIsValid = vm.sicrParameters.isValid;
            vm.viewModelHelper.modelErrors = vm.sicrParameters.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/sicrparameters/updatesicrparameters', vm.sicrParameters,
               function (result) {
                   
                   $state.go('ifrs-sicrparameters-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.sicrParameters.errors;

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
                vm.viewModelHelper.apiPost('api/sicrparameters/deletesicrparameters', vm.sicrParameters.ID,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-sicrparameters-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-sicrparameters-list');
        };

 
      //  sicrparametersRules();
        initialize(); 
    }
}());
