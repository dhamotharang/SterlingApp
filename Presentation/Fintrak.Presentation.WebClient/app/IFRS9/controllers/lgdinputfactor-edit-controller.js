/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("LgdInputFactorEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        LgdInputFactorEditController]);

    function LgdInputFactorEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'lgdinputfactor-edit-view';
        vm.viewName = 'Collateral Input';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.lgdInputFactors = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var lgdinputfactorRules = [];

        var setupRules = function () {

            lgdinputfactorRules.push(new validator.PropertyRule("Business", {
                required: { message: "Business is required" }
            }));

            lgdinputfactorRules.push(new validator.PropertyRule("Value", {
                required: { message: "Value is required" }
            }));

            lgdinputfactorRules.push(new validator.PropertyRule("Beststimate", {
                required: { message: "Beststimate is required" }
            }));

            lgdinputfactorRules.push(new validator.PropertyRule("Optimistic", {
                required: { message: "Optimistic is required" }
            }));

            lgdinputfactorRules.push(new validator.PropertyRule("Downturn", {
                required: { message: "Downturn is required" }
            }));

            lgdinputfactorRules.push(new validator.PropertyRule("Rundate", {
                required: { message: "Rundate is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.Collateral_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/lgdinputfactor/getlgdinputfactor/' + $stateParams.Collateral_Id, null,
                   function (result) {
                       vm.lgdInputFactors = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.lgdInputFactors = { Business: '', Value: '', Beststimate: '', Optimistic: '', Downturn: '', Rundate: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.lgdInputFactors, lgdinputfactorRules);
            vm.viewModelHelper.modelIsValid = vm.lgdInputFactors.isValid;
            vm.viewModelHelper.modelErrors = vm.lgdInputFactors.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/lgdinputfactor/updatelgdinputfactor', vm.lgdInputFactors,
               function (result) {
                   
                   $state.go('ifrs9-lgdinputfactor-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.lgdInputFactors.errors;

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
                vm.viewModelHelper.apiPost('api/lgdinputfactor/deletelgdinputfactor', vm.lgdInputFactors.Collateral_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-lgdinputfactor-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-lgdinputfactor-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
