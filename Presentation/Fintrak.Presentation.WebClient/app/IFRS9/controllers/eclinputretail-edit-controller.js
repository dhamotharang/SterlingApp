/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ECLInputRetailEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        ECLInputRetailEditController]);

    function ECLInputRetailEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'eclinputretail-edit-view';
        vm.viewName = 'ECL Input- Retail'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.eclinputretail = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

  
        var eclinputretailRules = [];

        

        var setupRules = function () {
          
            eclinputretailRules.push(new validator.PropertyRule("account_number", {
                required: { message: "Account Nuumber is required" }
            }));


            eclinputretailRules.push(new validator.PropertyRule("Stage", {
                required: { message: "Stage is required" }
            }));


            eclinputretailRules.push(new validator.PropertyRule("EIR", {
                required: { message: "EIR is required" }
            }));

            eclinputretailRules.push(new validator.PropertyRule("Amount", {
                required: { message: "Amount is required" }
            }));
       
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.eclInputRetailId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/eclinputretail/geteclinputretail/' + $stateParams.eclInputRetailId, null,
                   function (result) {
                       vm.eclinputretail = result.data;

                       initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.eclinputretail = { account_number: '', Stage: 0, EIR: 0, SeriesValue:0, Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.eclinputretail, eclinputretailRules);
            vm.viewModelHelper.modelIsValid = vm.eclinputretail.isValid;
            vm.viewModelHelper.modelErrors = vm.eclinputretail.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/eclinputretail/updateeclinputretail', vm.eclinputretail,
               function (result) {
                   
                   $state.go('ifrs-eclinputretail-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.eclinputretail.errors;

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
                vm.viewModelHelper.apiPost('api/eclinputretail/deleteeclinputretail', vm.eclinputretail.SectorId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-eclinputretail-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-eclinputretail-list');
        };




        setupRules();
        initialize(); 
    }
}());
