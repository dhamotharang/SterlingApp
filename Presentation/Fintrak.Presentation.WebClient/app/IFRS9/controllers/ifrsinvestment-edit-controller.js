/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsInvestmentEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsInvestmentEditController]);

    function IfrsInvestmentEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsinvestment-edit-view';
        vm.viewName = 'IfrsInvestment'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

       
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var InvestmentRules = [];

        var setupRules = function () {

            InvestmentRules.push(new validator.PropertyRule("INVType_Code",
                {
                    required: { message: "Investment Type Code is required" }
                }));

            InvestmentRules.push(new validator.PropertyRule("TypeCode",
                {
                    required: { message: "Type Code is required" }
                }));

        };

    

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                //intializeLookUp();

                if ($stateParams.Id !== 0) {
                  vm.showChildren = true;
                  vm.viewModelHelper.apiGet('api/ifrsinvestment/getifrsinvestment/' + $stateParams.Id, null,
                   function (result) {
                       vm.ifrsinvestment = result.data;
                      initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsinvestment = { Source: '',  Description: '', Active: true };
            }
        }
        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsinvestment, InvestmentRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsinvestment.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsinvestment.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrsinvestment/updateifrsinvestment', vm.ifrsinvestment,
               function (result) {
                   
                   $state.go('ifrs-ifrsinvestment-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsinvestment.errors;

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
              vm.viewModelHelper.apiPost('api/ifrsinvestment/deleteifrsinvestment', vm.ifrsinvestment.Id,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-ifrsinvestment-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs-ifrsinvestment-list');
        };

        setupRules();
        initialize(); 
    }
}());
