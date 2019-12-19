/**
 * Created by Tosin on 8/12/2019.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsMonthlyEADEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsMonthlyEADEditController]);

    function IfrsMonthlyEADEditController($scope,$window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IFRS9';
        vm.view = 'ifrsbondsmonthlyead-edit-view';
        vm.viewName = 'IfrsBondsMonthlyEAD'

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsbondsmonthlyead = {};
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

    

        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.Id !== 0) {
                    vm.showChildren = true;
                  vm.viewModelHelper.apiGet('api/ifrsbondsmonthlyead/getifrsbondsmonthlyead/{Id}' + $stateParams.Id, null,
                   function (result) {
                       vm.ifrsbondsmonthlyead = result.data;

                      initialView();
                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsbondsmonthlyead = { Source: '',  Description: '', Active: true };
            }
        }

        var intializeLookUp = function () {
           
        }

        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsbondsmonthlyead, ifrsbondsmonthlyeadRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsbondsmonthlyead.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsbondsmonthlyead.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
              vm.viewModelHelper.apiPost('api/ifrsbondsmonthlyead/updateifrsbondsmonthlyead', vm.ifrsbondsmonthlyead,
               function (result) {
                   
                   $state.go('ifrs9-ifrsbondsmonthlyead-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsbondsmonthlyead.errors;

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
              vm.viewModelHelper.apiPost('api/ifrsbondsmonthlyead/deleteifrsbondsmonthlyead', vm.ifrsbondsmonthlyead.IfrsBondsMonthlyEADId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs9-ifrsbondsmonthlyead-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        vm.cancel = function () {
            $state.go('ifrs9-ifrsbondsmonthlyead-list');
        };

        setupRules();
        initialize(); 
    }
}());
