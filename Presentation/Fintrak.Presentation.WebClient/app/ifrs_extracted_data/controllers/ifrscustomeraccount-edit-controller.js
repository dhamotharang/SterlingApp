/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("IfrsCustomerAccountEditController",
                    ['$scope', '$window', '$state', '$stateParams', 'viewModelHelper', 'validator',
                        IfrsCustomerAccountEditController]);

    function IfrsCustomerAccountEditController($scope, $window, $state, $stateParams, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'IfrsCustomerAccount';
        vm.view = 'ifrscustomeraccount-edit-view';
        vm.viewName = 'IFRS Customer Account Edit';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.ifrsCustomerAccount = {};
        vm.distinctSector = [];
        
        
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';
 
        var ifrsCustomerAccountRules = [];

          

        var setupRules = function () {
          
            ifrsCustomerAccountRules.push(new validator.PropertyRule("CustomerNo", {
                required: { message: "Customer Number is required" }
            }));

            ifrsCustomerAccountRules.push(new validator.PropertyRule("AccountNo", {
                required: { message: "Account Number is required" }
            }));

            ifrsCustomerAccountRules.push(new validator.PropertyRule("AccountName", {
                required: { message: "Account Name is required" }
            }));

        }


        var intializeLookUp = function () {
            distinctSector();
           
        }
        var initialize = function () {
            if (vm.init === false) {
                //load lookups
                intializeLookUp();

                if ($stateParams.ifrsCustomerAccountId !== 0) {
                    vm.showChildren = true;
                    vm.viewModelHelper.apiGet('api/ifrscustomeraccount/getcustomer/' + $stateParams.ifrsCustomerAccountId, null,
                   function (result) {
                       vm.ifrsCustomerAccount = result.data;
                       initialView();

                       vm.init === true;
                       
                   },
                   function (result) {
                       toastr.error(result.data, 'Fintrak');
                   }, null);
               }
               else
                    vm.ifrsCustomerAccount = { CustomerNo: '', AccountName: '', AccountNo: '', Active: true };
            }
        }


        var initialView = function () {
            
        }

        vm.save = function () {
            //Validate
            validator.ValidateModel(vm.ifrsCustomerAccount, ifrsCustomerAccountRules);
            vm.viewModelHelper.modelIsValid = vm.ifrsCustomerAccount.isValid;
            vm.viewModelHelper.modelErrors = vm.ifrsCustomerAccount.errors;
            if (vm.viewModelHelper.modelIsValid) {
             
                vm.viewModelHelper.apiPost('api/ifrscustomeraccount/updateifrscustomeraccount', vm.ifrsCustomerAccount,
               function (result) {
                   
                   $state.go('ifrs-ifrscustomeraccount-list');
               },
               function (result) {
                   toastr.error(result.data, 'Fintrak');
               }, null);
            }
            else
            {
                vm.viewModelHelper.modelErrors = vm.ifrsCustomerAccount.errors;

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
                vm.viewModelHelper.apiPost('api/ifrscustomeraccount/deleteifrscustomeraccount', vm.ifrsCustomerAccount.CustAccountId,
              function (result) {
                  toastr.success('Selected item deleted.', 'Fintrak');
                  $state.go('ifrs-ifrscustomeraccount-list');
              },
              function (result) {
                  toastr.error(result.data, 'Fintrak');
              }, null);
            } 
        }

        var distinctSector = function () {
            vm.viewModelHelper.apiGet('api/ifrscustomeraccount/getdistinctsector', null,
                 function (result) {
                     vm.distinctSector = result.data;
                 },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        vm.cancel = function () {
            $state.go('ifrs-ifrscustomeraccount-list');
        };              
       
        setupRules();
        initialize(); 
    }
}());
