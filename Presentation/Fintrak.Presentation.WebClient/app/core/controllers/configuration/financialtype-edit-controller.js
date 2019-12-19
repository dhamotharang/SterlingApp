/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FinancialTypeEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        FinancialTypeEditController]);

    function FinancialTypeEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'financialtype-edit-view';
        vm.viewName = 'Financial Type';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.financialType = {};
        vm.financialTypes = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var financialTypeRules = [];

        var setupRules = function () {
          
            financialTypeRules.push(new validator.PropertyRule("Name", {
                required: { message: "Name is required" }
            }));
        }

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                initialLookUp();

                if ($stateParams.financialtypeId !== 0) {
                    vm.showPeriod = true;
                    vm.viewModelHelper.apiGet('api/financialType/getfinancialType/' + $stateParams.financialtypeId, null,
                   function (result) {
                       vm.financialType = result.data;

                       initialView();
                       vm.init === true;
                       //
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.financialType = { Name: '',Code:'', Active: true };
            }
        }

        var initialLookUp = function () {
            getFinancialTypes();
        }

        var initialView = function () {
  
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.financialType, financialTypeRules);
            vm.viewModelHelper.modelIsValid = vm.financialType.isValid;
            vm.viewModelHelper.modelErrors = vm.financialType.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/financialType/updatefinancialType', vm.financialType,
               function (result) {
                   
                   $state.go('core-financialtype-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.financialType.errors;

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
                vm.viewModelHelper.apiPost('api/financialType/deletefinancialType', vm.financialType.FinancialTypeId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('core-financialtype-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('core-financialtype-list');
        };

        var getFinancialTypes = function () {
            vm.viewModelHelper.apiGet('api/financialtype/availablefinancialtypes', null,
                 function (result) {
                     vm.financialTypes = result.data;
 
                     vm.init === true;

                 },
                 function (result) {
                     toastr.error('Fail to load financial types.', 'Fintrak');
                 }, null);
        }

        setupRules();
        initialize(); 
    }
}());
