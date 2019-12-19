/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("CollateralInputEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        CollateralInputEditController]);

    function CollateralInputEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'collateralinput-edit-view';
        vm.viewName = 'Collateral Input';
        vm.status = false;

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.collateralInputs = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var collateralinputRules = [];

        var setupRules = function () {

            collateralinputRules.push(new validator.PropertyRule("Year", {
                required: { message: "type is required" }
            }));

            collateralinputRules.push(new validator.PropertyRule("code", {
                required: { message: "code is required" }
            }));

            collateralinputRules.push(new validator.PropertyRule("Collateral_haircut", {
                required: { message: "Collateral_haircut is required" }
            }));

            collateralinputRules.push(new validator.PropertyRule("Collateral_Growth_rate", {
                required: { message: "Collateral_Growth_rate is required" }
            }));

            collateralinputRules.push(new validator.PropertyRule("Cost_of_recovery", {
                required: { message: "Cost_of_recovery is required" }
            }));

            collateralinputRules.push(new validator.PropertyRule("Time_of_recovery", {
                required: { message: "Time_of_recovery is required" }
            }));

            collateralinputRules.push(new validator.PropertyRule("catergory", {
                required: { message: "catergory is required" }
            }));

        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.Collateral_Id !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/collateralinput/getcollateralinput/' + $stateParams.Collateral_Id, null,
                   function (result) {
                       vm.collateralInputs = result.data;
                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
                }
                else
                    vm.collateralInputs = { type: '', code: '', Collateral_haircut: '',  Collateral_Growth_rate: '',  Cost_of_recovery: '',  Time_of_recovery: '',  catergory: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.collateralInputs, collateralinputRules);
            vm.viewModelHelper.modelIsValid = vm.collateralInputs.isValid;
            vm.viewModelHelper.modelErrors = vm.collateralInputs.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/collateralinput/updatecollateralinput', vm.collateralInputs,
               function (result) {
                   
                   $state.go('ifrs9-collateralinput-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.collateralInputs.errors;

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
                vm.viewModelHelper.apiPost('api/collateralinput/deletecollateralinput', vm.collateralInputs.Collateral_Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-collateralinput-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-collateralinput-list');
        };

        
        setupRules();
        initialize(); 
    }
}());
